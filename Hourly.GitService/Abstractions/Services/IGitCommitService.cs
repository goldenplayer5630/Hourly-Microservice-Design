using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Abstractions.Services
{
    public interface IGitCommitService
    {
        Task<IEnumerable<GitCommit>> GetAll();
        Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate);
        Task<GitCommit> GetById(Guid gitCommitid);
        Task<GitCommit> Create(GitCommit gitCommit);
        Task Delete(Guid gitCommitid);
    }
}
