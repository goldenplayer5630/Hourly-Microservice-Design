using Hourly.GitService.Abstractions.Queries;
using Hourly.GitService.Abstractions.Repositories;
using Hourly.GitService.Abstractions.Services;
using Hourly.GitService.Domain.Entities;
using Hourly.Shared.Exceptions;

namespace Hourly.GitService.Application.Services
{
    public class GitCommitService : IGitCommitService
    {
        private readonly IGitCommitRepository _repository;
        private readonly IGitRepositoryRepository _gitRepositoryRepository;
        private readonly IUserQuery _userQuery;

        public GitCommitService(IGitCommitRepository repository, IGitRepositoryRepository gitRepositoryRepository, IUserQuery userQuery)
        {
            _repository = repository;
            _gitRepositoryRepository = gitRepositoryRepository;
            _userQuery = userQuery;
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

            var gitRepository = await _gitRepositoryRepository.GetById(gitCommit.GitRepositoryId)
                ?? throw new EntityNotFoundException("GitRepository not found!");

            var author = await _userQuery.GetById(gitCommit.AuthorId)
                ?? throw new EntityNotFoundException("User not found!");

            gitCommit.AssignToRepository(gitRepository);
            gitCommit.AssignToAuthor(author);

            return await _repository.Create(gitCommit);
        }

        public async Task Delete(Guid gitCommitId)
        {
            await _repository.Delete(gitCommitId);
        }
    }
}
