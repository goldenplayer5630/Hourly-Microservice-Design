using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitCommitConsumers
{
    public class GitCommitCreatedConsumer : IConsumer<GitCommitCreatedEvent>
    {
        private readonly AppDbContext _db;
        public GitCommitCreatedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<GitCommitCreatedEvent> context)
        {
            var msg = context.Message;
            if (await _db.GitCommits.FindAsync(msg.Id) is not null)
                return;
            var commit = new GitCommitReadModel
            {
                Id = msg.Id,
                AuthorId = msg.AuthorId,
            };
            _db.GitCommits.Add(commit);
            await _db.SaveChangesAsync();
        }
    }
}
