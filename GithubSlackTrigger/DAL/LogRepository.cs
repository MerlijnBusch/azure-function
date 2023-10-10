using GithubSlackTrigger.DAL.Interface;
using GithubSlackTrigger.Model;
using Microsoft.Extensions.Logging;

namespace GithubSlackTrigger.DAL
{
    public class LogRepository : BaseRepository<RequestModel>, ILogRepository
    {
        private readonly ILogger _logger;
        public LogRepository(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BaseRepository<RequestModel>>();
            _table = _tableClient.GetTableReference("logs");
            _table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }
    }
}
