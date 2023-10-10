using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Model
{
    public class IncData
    {
        public string? commitHash { get; set; }
        public string? commitBy { get; set; }
        public string? url { get; set; }
        public string? branch { get; set; }
        public string? message { get; set; }
        public string? timestamp { get; set; }
    }
}
