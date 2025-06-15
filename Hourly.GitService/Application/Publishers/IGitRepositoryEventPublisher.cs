using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Application.Publishers
{
    public interface IGitRepositoryEventPublisher
    {
        Task PublishGitRepositoryCreated(GitRepository GitCommit);
        Task PublishGitRepositoryUpdated(GitRepository GitCommit);
        Task PublishGitRepositoryDeleted(Guid GitCommitId);
    }
}
