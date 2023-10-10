using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.Model
{
    public class ErrorModel : TableEntity
    {
        public string? message { get; set; }
        public ErrorModel()
        {

        }

        public ErrorModel(string? message)
        {
            this.message = message;

            PartitionKey = "Error";
            RowKey = Guid.NewGuid().ToString();
        }
    }
}
