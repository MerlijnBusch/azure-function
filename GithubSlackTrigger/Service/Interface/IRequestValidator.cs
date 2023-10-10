using GithubSlackTrigger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Service.Interface
{
    public interface IRequestValidator
    {
        public Task<RequestModel> CreateValidatedRequestModelAsync(
            string? commitHash, string? commitBy, string? url, string? branch, string? message, string? timestamp);
    }
}
