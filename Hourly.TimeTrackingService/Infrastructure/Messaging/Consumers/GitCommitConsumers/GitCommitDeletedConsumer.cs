using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitCommitConsumers
{
    public class GitCommitDeletedConsumer : IConsumer<GitCommitDeletedEvent>
    {
        private readonly AppDbContext _db;
        public GitCommitDeletedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<GitCommitDeletedEvent> context)
        {
            var msg = context.Message;
            var commit = await _db.GitCommits.FindAsync(msg.Id);
            if (commit is null)
                return;
            _db.GitCommits.Remove(commit);
            await _db.SaveChangesAsync();
        }
    }
}
