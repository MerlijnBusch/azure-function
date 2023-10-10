using GithubSlackTrigger.DAL.Interface;
using GithubSlackTrigger.Model;
using Microsoft.Extensions.Logging;

namespace GithubSlackTrigger.DAL
{
    public class LogErrorRepository : BaseRepository<ErrorModel>, ILogErrorRepository
    {
        private readonly ILogger _logger;
        public LogErrorRepository(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BaseRepository<ErrorModel>>();
            _table = _tableClient.GetTableReference("ErrorLogs");
            _table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }
    }
}
