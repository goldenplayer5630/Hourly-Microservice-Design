using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Abstractions.Repositories
{
    public interface IGitCommitRepository
    {
        Task<GitCommit?> GetById(Guid gitCommitId);
        Task<IEnumerable<GitCommit>> GetAll();
        Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitId);
        Task SaveChanges();
    }
}
