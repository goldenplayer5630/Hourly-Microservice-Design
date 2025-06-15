using Hourly.GitService.Domain.Entities;
using Hourly.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hourly.GitService.Infrastructure.Repositories
{
    public class GitRepositoryRepository : IGitRepositoryRepository
    {
        private readonly AppDbContext _context;

        public GitRepositoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GitRepository?> GetById(Guid gitRepositoryId)
        {
            return await _context.GitRepositories
                .Include(x => x.GitCommits)
                .FirstOrDefaultAsync(gr => gr.Id == gitRepositoryId);
        }

        public async Task<IEnumerable<GitRepository>> GetAll()
        {
            return await _context.GitRepositories
                .Include(x => x.GitCommits)
                .ToListAsync();
        }

        public async Task<GitRepository> Create(GitRepository gitRepository)
        {
            await _context.GitRepositories.AddAsync(gitRepository);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? gitRepository : null) ?? throw new InvalidOperationException();
        }

        public async Task<GitRepository> Update(GitRepository gitRepository)
        {
            var existingGitRepository = await _context.GitRepositories.FindAsync(gitRepository.Id);
            if (existingGitRepository == null)
            {
                throw new EntityNotFoundException("Git repository not found!");
            }

            _context.Entry(existingGitRepository).CurrentValues.SetValues(gitRepository);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? gitRepository : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid gitRepositoryId)
        {
            var existingGitRepository = await _context.GitRepositories.FindAsync(gitRepositoryId);
            if (existingGitRepository == null)
            {
                throw new EntityNotFoundException("Git repository not found!");
            }

            _context.GitRepositories.Remove(existingGitRepository);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
