namespace TestApp.Data.Interfaces
{
    using Microsoft.Azure.Cosmos.Table;
    using System.Threading.Tasks;
    using TestApp.Data.Models;
    public interface IPersonDatabaseRepository
    {
        Task<CloudStorageAccount> CreateStorageAccountAsync();
        Task<CloudTable> CreateTableAsync(string name);
        Task<object> InsertOrMergePersonAsync(PersonModel person);
        Task<PersonModel> GetPersonByIdAsync(string table, string id);
        Task<CloudTable> GetTableAsync(string tableName);
        Task<TEntity> DeleteEntityAsync<TEntity>(CloudTable table, TEntity entity) where TEntity: class;
    }
}