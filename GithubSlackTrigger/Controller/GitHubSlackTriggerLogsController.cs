using System.Net;
using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service;
using GithubSlackTrigger.Service.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace GithubSlackTrigger.Controller
{
    public class GitHubSlackTriggerLogsController
    {
        private readonly ILogger _logger;
        private readonly ILogService _logService;

        public GitHubSlackTriggerLogsController(ILoggerFactory loggerFactory, ILogService logService)
        {
            _logger     = loggerFactory.CreateLogger<GitHubSlackTriggerLogsController>();
            _logService = logService;
        }

        [Function("logs")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            HttpResponseData? response;
            try
            {
                IEnumerable<RequestModel>? data = await _logService.GetAll();
                response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} at {ex.StackTrace}, details: {ex.GetBaseException()}");
                response = req.CreateResponse(HttpStatusCode.UnprocessableEntity);
            }

            return response;
        }
    }
}
