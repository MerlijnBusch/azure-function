using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service.Interface;
using Microsoft.Extensions.Logging;
using System.Text;

namespace GithubSlackTrigger.Service
{
    public class SendSlackMessage : ISendSlackMessage
    {
        private readonly ILogger _logger;
        private readonly ILogErrorService _logErrorService;

        public SendSlackMessage(ILoggerFactory loggerFactory, ILogErrorService logErrorService)
        {
            _logger          = loggerFactory.CreateLogger<SendSlackMessage>();
            _logErrorService = logErrorService;
        }

        public async Task init(string data)
        {
            string slackWebhookUrl       = GetSlackWebhookUrlFromConfigurations();
            using HttpClient client      = new HttpClient();
            var content                  = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(slackWebhookUrl, content);

            if (! response.IsSuccessStatusCode)
            {
                await _logErrorService.Add(new ErrorModel("slack message did not send"));
            }
            
        }

        private string GetSlackWebhookUrlFromConfigurations()
        {
            string? url = Environment.GetEnvironmentVariable("MySlackURL");

            if (url == null)
            {
                throw new Exception();
            }

            return url;
        }
    }
}
