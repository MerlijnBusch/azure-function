using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Service.Interface
{
    public interface ISendSlackMessage
    {
        public Task init(string data);
    }
}
