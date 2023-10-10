using GithubSlackTrigger.Model;
using GithubSlackTrigger.Service.Interface;

namespace GithubSlackTrigger.Service
{
    public class RequestValidator : IRequestValidator
    {
        private readonly ILogErrorService _logErrorService;

        public RequestValidator(ILogErrorService logErrorService) {
            _logErrorService = logErrorService;
        }

        public async Task<RequestModel> CreateValidatedRequestModelAsync(
             string? commitHash, string? commitBy, string? url, string? branch, string? message, string? timestamp)
        {
            commitHash = await ValidateAndSanitizeAsync(commitHash);
            commitBy   = await ValidateAndSanitizeAsync(commitBy);
            branch     = await ValidateAndSanitizeAsync(branch);
            message    = await ValidateAndSanitizeAsync(message);
            url        = await ValidateAndSanitizeUrlAsync(url);
            timestamp  = await ValidateAndSanitizeTimestampAsync(timestamp);

            RequestModel requestModel = new RequestModel(
                commitHash,
                commitBy,
                url,
                branch,
                message,
                timestamp
            );

            return requestModel;
        }


        private async Task<string> ValidateAndSanitizeAsync(string? input)
        {
            if (input == null)
            {
                await _logErrorService.Add(new ErrorModel("Cannot sanitize input"));
                throw new Exception();
            }

            input = input.Trim();

            return input;
        }


        private async Task<string> ValidateAndSanitizeUrlAsync(string? url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out _))
            {
                return url;
            }

            await _logErrorService.Add(new ErrorModel("Cannot sanatize url"));
            throw new Exception();
        }

        private async Task<string> ValidateAndSanitizeTimestampAsync(string? timestamp)
        {
            if (DateTime.TryParse(timestamp, out _))
            {
                return timestamp;
            }

            await _logErrorService.Add(new ErrorModel("Invalid date time provided"));
            throw new Exception();
        }
    }
}
