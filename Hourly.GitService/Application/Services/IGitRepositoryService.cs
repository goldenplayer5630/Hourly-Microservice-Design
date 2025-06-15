using Hourly.GitService.Domain.Entities;

namespace Hourly.GitService.Abstractions.Services
{
    public interface IGitRepositoryService
    {
        Task<IEnumerable<GitRepository>> GetAll();
        Task<GitRepository> GetById(Guid id);
        Task<GitRepository> Create(GitRepository gitRepository);
        Task<GitRepository> Update(GitRepository gitRepository);
        Task Delete(Guid id);
    }
}
