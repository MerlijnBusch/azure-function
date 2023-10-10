using GithubSlackTrigger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Service.Interface
{
    public interface ILogService
    {
        public Task Add(RequestModel request);

        public Task<IEnumerable<RequestModel>> GetAll();
    }
}
