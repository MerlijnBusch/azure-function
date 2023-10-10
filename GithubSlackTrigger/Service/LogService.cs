using GithubSlackTrigger.DAL.Interface;
using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service.Interface;
using Microsoft.Extensions.Logging;

namespace GithubSlackTrigger.Service
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger;
        private readonly ILogRepository _logRepository;

        public LogService(ILoggerFactory loggerFactory, ILogRepository logRepository)
        {
            _logger        = loggerFactory.CreateLogger<LogService>();
            _logRepository = logRepository;
        }

        public async Task Add(RequestModel request)
        {
            _logger.LogInformation("in service");

            await _logRepository.CreateAsync(request);
        }

        public async Task<IEnumerable<RequestModel>> GetAll()
        {
            return await _logRepository.GetAllAsync();
        }
    }
}
