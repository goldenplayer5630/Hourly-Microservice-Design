using Hourly.GitService.Application.Publishers;
using Hourly.GitService.Domain.Entities;
using Hourly.GitService.Infrastructure.Queries;
using Hourly.GitService.Infrastructure.Repositories;
using Hourly.Shared.Exceptions;

namespace Hourly.GitService.Application.Services
{
    public class GitCommitService : IGitCommitService
    {
        private readonly IGitCommitRepository _repository;
        private readonly IGitRepositoryRepository _gitRepositoryRepository;
        private readonly IUserQuery _userQuery;
        private readonly IGitCommitEventPublisher _gitCommitEventPublisher;

        public GitCommitService(IGitCommitRepository repository, IGitRepositoryRepository gitRepositoryRepository, IUserQuery userQuery, IGitCommitEventPublisher gitCommitEventPublisher)
        {
            _repository = repository;
            _gitRepositoryRepository = gitRepositoryRepository;
            _userQuery = userQuery;
            _gitCommitEventPublisher = gitCommitEventPublisher;
        }

        public async Task<GitCommit> GetById(Guid gitCommitId)
        {
            return await _repository.GetById(gitCommitId)
                ?? throw new EntityNotFoundException("GitCommit not found!");
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate)
        {
            return await _repository.Filter(repositoryId, authorId, authoredDate);
        }

        public async Task<GitCommit> Create(GitCommit gitCommit)
        {
            gitCommit.Id = Guid.NewGuid();
            gitCommit.CreatedAt = DateTime.UtcNow;

            _ = await _gitRepositoryRepository.GetById(gitCommit.GitRepositoryId)
                ?? throw new EntityNotFoundException("GitRepository not found!");

            if (gitCommit.AuthorId is not null && gitCommit.AuthorId.HasValue)
            {
                _ = await _userQuery.GetById(gitCommit.AuthorId.Value)
                    ?? throw new EntityNotFoundException("User not found!");
            }

            return await _repository.Create(gitCommit);
        }

        public async Task Delete(Guid gitCommitId)
        {
            await _repository.Delete(gitCommitId);

            await _gitCommitEventPublisher.PublishGitCommitDeleted(gitCommitId);
        }
    }
}
