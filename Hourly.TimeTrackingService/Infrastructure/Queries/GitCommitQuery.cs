using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Queries
{
    public class GitCommitQuery : IGitCommitQuery
    {
        private readonly AppDbContext _context;

        public GitCommitQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Exists(Guid commitId)
        {
            return await _context.GitCommits.AnyAsync(c => c.Id == commitId);
        }

        public async Task<List<GitCommitReadModel>> GetByIds(IEnumerable<Guid> gitCommitIds)
        {
            var ids = gitCommitIds.Distinct().ToList();
            return await _context.GitCommits
                .Where(c => ids.Contains(c.Id))
                .Include(c => c.GitRepository)
                .Include(c => c.Author)
                .ToListAsync();
        }

        public async Task<GitCommitReadModel?> GetById(Guid commitId)
        {
            return await _context.GitCommits
                .Include(c => c.GitRepository)
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.Id == commitId);
        }
    }
}
