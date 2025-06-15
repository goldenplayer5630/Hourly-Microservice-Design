using Hourly.GitService.Domain.Entities;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.GitService.Application.Publishers
{
    public class GitCommitEventPublisher : IGitCommitEventPublisher
    {
        private readonly IPublishEndpoint _publish;
        public GitCommitEventPublisher(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public async Task PublishGitCommitCreated(GitCommit gitCommit)
        {
            await _publish.Publish(new GitCommitCreatedEvent(gitCommit.Id, gitCommit.AuthorId, gitCommit.AuthoredDate, gitCommit.GitRepositoryId, gitCommit.ExtCommitId, gitCommit.ExtCommitShortId, gitCommit.Title, gitCommit.WebUrl));
        }

        public async Task PublishGitCommitDeleted(Guid gitCommitId)
        {
            await _publish.Publish(new GitCommitDeletedEvent(gitCommitId));
        }
    }
}

