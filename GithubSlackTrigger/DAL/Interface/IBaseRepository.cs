using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubSlackTrigger.DAL.Interface
{
    public interface IBaseRepository
    {
        public interface IBaseRepository<T> where T : TableEntity, new()
        {
            Task CreateAsync(T entity);
        }
    }
}
