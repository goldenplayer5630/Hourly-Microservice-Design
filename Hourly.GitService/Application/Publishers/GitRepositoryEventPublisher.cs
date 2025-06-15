using Hourly.GitService.Domain.Entities;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.GitService.Application.Publishers
{
    public class GitRepositoryEventPublisher : IGitRepositoryEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public GitRepositoryEventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishGitRepositoryCreated(GitRepository gitRepository)
        {
            await _publishEndpoint.Publish(new GitRepositoryCreatedEvent(gitRepository.Id, gitRepository.ExtRepositoryId, gitRepository.Name, gitRepository.WebUrl));
        }

        public async Task PublishGitRepositoryUpdated(GitRepository gitRepository)
        {
            await _publishEndpoint.Publish(new GitRepositoryUpdatedEvent(gitRepository.Id, gitRepository.ExtRepositoryId, gitRepository.Name, gitRepository.WebUrl));
        }

        public async Task PublishGitRepositoryDeleted(Guid gitRepositoryId)
        {
            await _publishEndpoint.Publish(new GitRepositoryDeletedEvent(gitRepositoryId));
        }
    }
}
