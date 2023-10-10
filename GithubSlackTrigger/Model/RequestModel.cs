using Microsoft.WindowsAzure.Storage.Table;

namespace GithubSlackTrigger.Model
{
    public class RequestModel : TableEntity
    {
        public string? commitHash { get; set; }
        public string? commitBy { get; set; }
        public string? url { get; set; }
        public string? branch { get; set; }
        public string? message { get; set; }
        public string? timestamp { get; set; }

        public RequestModel()
        {

        }

        public RequestModel(string? commitHash, string? commitBy, string? url, string? branch, string? message, string? timestamp)
        {
            this.commitHash = commitHash;
            this.commitBy = commitBy;
            this.url = url;
            this.branch = branch;
            this.message = message;
            this.timestamp = timestamp;

            PartitionKey = branch;
            RowKey = commitHash;
        }
    }
}
