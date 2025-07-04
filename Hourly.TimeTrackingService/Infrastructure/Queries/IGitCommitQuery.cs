﻿using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Infrastructure.Queries
{
    public interface IGitCommitQuery
    {
        Task<bool> Exists(Guid commitId);
        Task<List<GitCommitReadModel>> GetByIds(IEnumerable<Guid> commitIds);
        Task<GitCommitReadModel?> GetById(Guid commitId);
    }
}
