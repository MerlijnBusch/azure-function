using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using static GithubSlackTrigger.DAL.Interface.IBaseRepository;
using Microsoft.Extensions.Logging;

namespace GithubSlackTrigger.DAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : TableEntity, new()
    {
        private readonly ILogger _logger;
        private readonly CloudStorageAccount _storageAccount;
        protected CloudTableClient _tableClient;
        protected CloudTable? _table = null;

        public BaseRepository(ILoggerFactory loggerFactory) : base()
        {
            _logger         = loggerFactory.CreateLogger<BaseRepository<T>>();
            _storageAccount = CloudStorageAccount.Parse(this.GetDatabaseUrlFromConfigurations());
            _tableClient    = _storageAccount.CreateCloudTableClient();
        }

        public async Task CreateAsync(T entity)
        {
            _logger.LogInformation("create async");

            if (_table == null) {
                throw new InvalidOperationException();
            }

            _logger.LogInformation("create async after");

            try
            {
                TableOperation insertOperation = TableOperation.Insert(entity);
                await _table.ExecuteAsync(insertOperation);
            }
            catch (StorageException ex)
            {
                _logger.LogInformation("Error: " + ex.Message);
                throw; // Rethrow the exception to propagate it if needed.
            }
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> entities    = new();
            TableQuery<T> query = new();

            if (_table == null)
            {
                throw new InvalidOperationException();
            }

            foreach (T entity in _table.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                entities.Add(entity);
            }

            return Task.FromResult<IEnumerable<T>>(entities);
        }

        private string GetDatabaseUrlFromConfigurations()
        {
            string? url = Environment.GetEnvironmentVariable("MyDatabaseConnection");

            if (url == null)
            {
                throw new Exception();
            }

            return url;
        }
    }
}
