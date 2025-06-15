using Hourly.GitService.Application.Publishers;
using Hourly.GitService.Domain.Entities;
using Hourly.GitService.Infrastructure.Repositories;
using Hourly.Shared.Exceptions;

namespace Hourly.GitService.Application.Services
{
    public class GitRepositoryService : IGitRepositoryService
    {
        private readonly IGitRepositoryRepository _repository;
        private readonly IGitRepositoryEventPublisher _gitRepositoryEventPublisher;

        public GitRepositoryService(IGitRepositoryRepository repository, IGitRepositoryEventPublisher gitRepositoryEventPublisher)
        {
            _repository = repository;
            _gitRepositoryEventPublisher = gitRepositoryEventPublisher;
        }

        public async Task<GitRepository> GetById(Guid gitRepositoryId)
        {
            return await _repository.GetById(gitRepositoryId)
                ?? throw new EntityNotFoundException("GitRepository not found!");
        }

        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<GitRepository> Create(GitRepository gitRepository)
        {
            gitRepository.Id = Guid.NewGuid();
            gitRepository.CreatedAt = DateTime.UtcNow;



            var result = await _repository.Create(gitRepository);

            await _gitRepositoryEventPublisher.PublishGitRepositoryCreated(result);

            return result;
        }

        public async Task<GitRepository> Update(GitRepository gitRepository)
        {
            gitRepository.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.Update(gitRepository);

            await _gitRepositoryEventPublisher.PublishGitRepositoryUpdated(result);

            return result;
        }

        public async Task Delete(Guid gitRepositoryId)
        {
            await _repository.Delete(gitRepositoryId);

            await _gitRepositoryEventPublisher.PublishGitRepositoryDeleted(gitRepositoryId);
        }
    }
}
