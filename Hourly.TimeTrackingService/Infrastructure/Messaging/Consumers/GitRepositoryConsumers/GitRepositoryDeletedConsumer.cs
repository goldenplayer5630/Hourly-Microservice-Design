using Hourly.Shared.Events;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitRepositoryConsumers
{
    public class GitRepositoryDeletedConsumer : IConsumer<GitRepositoryDeletedEvent>    
    {
        private readonly AppDbContext _db;

        public GitRepositoryDeletedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<GitRepositoryDeletedEvent> context)
        {
            var msg = context.Message;
            var repository = await _db.GitRepositories.FindAsync(msg.Id);
            if (repository is null)
                return;
            _db.GitRepositories.Remove(repository);

            var commits = await _db.GitCommits
                .Where(c => c.GitRepositoryId == msg.Id)
                .ToListAsync();

            if (commits is not null && commits.Any())
            {
                _db.GitCommits.RemoveRange(commits);
            }

            await _db.SaveChangesAsync();
        }
    }
}
