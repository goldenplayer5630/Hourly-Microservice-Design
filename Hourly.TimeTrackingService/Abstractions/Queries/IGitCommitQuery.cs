using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Abstractions.Queries
{
    public interface IGitCommitQuery
    {
        Task<bool> ExistsAsync(Guid commitId);
        Task<List<GitCommitReadModel>> GetByIdsAsync(IEnumerable<Guid> commitIds);
        Task<GitCommitReadModel?> GetById(Guid commitId);
    }
}
