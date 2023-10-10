using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GithubSlackTrigger.Controller
{
    public class GitHubSlackTriggerController
    {
        private readonly ILogger _logger;
        private readonly IRequestValidator _requestValidator;
        private readonly ILogService _logService;
        private readonly ISendSlackMessage _sendSlackMessage;

        public GitHubSlackTriggerController(
            ILoggerFactory loggerFactory, 
            IRequestValidator requestValidator, 
            ILogService logService, 
            ISendSlackMessage sendSlackMessage
        ) {
            _logger           = loggerFactory.CreateLogger<GitHubSlackTriggerController>();
            _requestValidator = requestValidator;
            _logService       = logService;
            _sendSlackMessage = sendSlackMessage;
        }

        [Function("post")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("Log req data");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            IncData data       = JsonConvert.DeserializeObject<IncData>(requestBody);

            string? commitHash = data.commitHash;
            string? commitBy   = data.commitBy;
            string? url        = data.url;
            string? branch     = data.branch;
            string? message    = data.message;
            string? timestamp  = data.timestamp;

            RequestModel requestModel = await _requestValidator.CreateValidatedRequestModelAsync(commitHash, commitBy, url, branch, message, timestamp);

            await _logService.Add(requestModel);

            string slackMessage = $"Commit Info:\n" +
                $"- Commit Hash: {requestModel.commitHash}\n" +
                $"- Commit By: {requestModel.commitBy}\n" +
                $"- URL: {requestModel.url}\n" +
                $"- Branch: {requestModel.branch}\n" +
                $"- Message: {requestModel.message}\n" +
                $"- Timestamp: {requestModel.timestamp}";

            await _sendSlackMessage.init(slackMessage);
        }
    }
}
