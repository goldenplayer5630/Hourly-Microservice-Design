using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Events;
using MassTransit;
using Hourly.TimeTrackingService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
                ExtCommitId = msg.ExtCommitId,
                ExtCommitShortId = msg.ExtCommitShortId,
                Title = msg.Title,
                AuthoredDate = msg.AuthoredDate,
                WebUrl = msg.WebUrl,
                GitRepositoryId = msg.GitRepositoryId
            };
            _db.GitCommits.Add(commit);
            await _db.SaveChangesAsync();
        }
    }
}