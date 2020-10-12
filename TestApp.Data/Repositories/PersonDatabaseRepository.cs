

namespace TestApp.Data.Repositories
{
    using Castle.Core.Configuration;
    using Castle.Core.Logging;
    using Microsoft.Azure.Cosmos.Table;
    using Microsoft.Azure.KeyVault;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using TestApp.Data.Helpers;
    using TestApp.Data.Interfaces;
    using TestApp.Data.Models;

    public class PersonDatabaseRepository : IPersonDatabaseRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<PersonDatabaseRepository> _logger;
        //private readonly IKeyVault keyVault;

        public PersonDatabaseRepository(IConfiguration config, ILogger<PersonDatabaseRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<CloudStorageAccount> CreateStorageAccountAsync()
        {
            CloudStorageAccount storageAccount;
            try
            {
                var dbConnectionString = "UseDevelopmentStorage=true";
                //var dbConnectionString = await _keyVault.GetSecreteAsync(_config["KeyvaultEndpoint"], Constants.AppSettings.TABLE_DB_CONNECTION_STRING).ConfigureAwait(false);
                storageAccount = CloudStorageAccount.Parse(dbConnectionString);
            }catch (FormatException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            catch(ArgumentException e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
            return storageAccount;
        }

        public Task<CloudTable> CreateTableAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> DeleteEntityAsync<TEntity>(CloudTable table, TEntity entity) where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonModel> GetPersonByIdAsync(string table, string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            var storageAccount = await CreateStorageAccountAsync();
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }

        public async Task<object> InsertOrMergePersonAsync(PersonModel person)
        {
            var table = Constants.TableDBConstant.TEST_APP_TABLE;
            var currentTable = await GetTableAsync(table).ConfigureAwait(false);
            try
            {
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(person);
                TableResult result = await currentTable.ExecuteAsync(insertOrMergeOperation);
                var stringResponse = JsonConvert.SerializeObject(result.Result);
                PersonModel response = JsonConvert.DeserializeObject<PersonModel>(stringResponse);
                return response;
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}