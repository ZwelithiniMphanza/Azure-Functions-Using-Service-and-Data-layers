using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;
using TestApp.Data.Interfaces;
using TestApp.Data.Models;
using TestApp.Service.Interfaces;

namespace TestApp.Service.Services
{
    public class PersonDatabaseService : IPersonDatabaseService
    {
        private readonly IPersonDatabaseRepository _personRepository;
        public PersonDatabaseService(IPersonDatabaseRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<CloudStorageAccount> CreateStorageAccountAsync()
        {
            return await _personRepository.CreateStorageAccountAsync().ConfigureAwait(false);
        }

        public async Task<CloudTable> CreateTableAsync(string name)
        {
            return await _personRepository.CreateTableAsync(name).ConfigureAwait(false);
        }

        public async Task<TEntity> DeleteEntityAsync<TEntity>(CloudTable table, TEntity entity) where TEntity : class
        {
            return await _personRepository.DeleteEntityAsync(table, entity).ConfigureAwait(false);
        }   

        public async Task<PersonModel> GetPersonByIdAsync(string table, string userId)
        {
            return await _personRepository.GetPersonByIdAsync(table, userId).ConfigureAwait(false);
        }

        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            return await _personRepository.GetTableAsync(tableName).ConfigureAwait(false);
        }

        public async Task<object> InsertOrMergePersonAsync(PersonModel person)
        {
            return await _personRepository.InsertOrMergePersonAsync(person).ConfigureAwait(false);
        }
    }
}