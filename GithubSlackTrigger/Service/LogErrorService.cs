using GithubSlackTrigger.DAL.Interface;
using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Service
{
    public class LogErrorService : ILogErrorService
    {
        private readonly ILogger _logger;
        private readonly ILogErrorRepository _logErrorRepository;

        public LogErrorService(ILoggerFactory loggerFactory, ILogErrorRepository logErrorRepository)
        {
            _logger             = loggerFactory.CreateLogger<LogErrorService>();
            _logErrorRepository = logErrorRepository;
        }

        public async Task Add(ErrorModel error)
        {
            _logger.LogInformation("in LogErrorService");

            await _logErrorRepository.CreateAsync(error);
        }
    }
}
