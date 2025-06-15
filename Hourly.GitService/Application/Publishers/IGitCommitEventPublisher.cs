using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Application.Publishers
{
    public interface IGitCommitEventPublisher
    {
        Task PublishGitCommitCreated(GitCommit GitCommit);
        Task PublishGitCommitDeleted(Guid GitCommitId);
    }
}
