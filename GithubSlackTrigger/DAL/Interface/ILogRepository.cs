﻿using GithubSlackTrigger.Model;
using static GithubSlackTrigger.DAL.Interface.IBaseRepository;

namespace GithubSlackTrigger.DAL.Interface
{
    public interface ILogRepository : IBaseRepository<RequestModel>
    {
    }
}
