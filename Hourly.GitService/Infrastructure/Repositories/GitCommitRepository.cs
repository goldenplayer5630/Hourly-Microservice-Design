using Hourly.GitService.Abstractions.Repositories;
using Hourly.GitService.Domain.Entities;
using Hourly.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hourly.GitService.Infrastructure.Repositories
{
    public class GitCommitRepository : IGitCommitRepository
    {
        private readonly AppDbContext _context;

        public GitCommitRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GitCommit?> GetById(Guid gitCommitId)
        {
            return await _context.GitCommits
                .Include(gc => gc.GitRepository)
                .Include(gc => gc.Author)
                .FirstOrDefaultAsync(gc => gc.Id == gitCommitId);
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _context.GitCommits
                .Include(gc => gc.GitRepository)
                .Include(gc => gc.Author)
                .ToListAsync();
        }

        public async Task<IEnumerable<GitCommit>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate)
        {
            var query = _context.GitCommits
                .Include(gc => gc.GitRepository)
                .Include(gc => gc.Author)
                .AsQueryable();

            if (repositoryId.HasValue)
                query = query.Where(gc => gc.GitRepositoryId == repositoryId.Value);

            if (authorId.HasValue)
                query = query.Where(gc => gc.AuthorId == authorId.Value);

            if (authoredDate.HasValue && authoredDate != default)
                query = query.Where(gc => gc.AuthoredDate.Date == authoredDate.Value.Date);

            return await query.ToListAsync();
        }

        public async Task<GitCommit> Create(GitCommit gitCommit)
        {
            await _context.GitCommits.AddAsync(gitCommit);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? gitCommit : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid gitCommitId)
        {
            var existingGitCommit = await _context.GitCommits.FindAsync(gitCommitId);

            if (existingGitCommit == null)
            {
                throw new EntityNotFoundException("Git commit not found!");
            }

            _context.GitCommits.Remove(existingGitCommit);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
