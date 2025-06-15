using Hourly.Shared.Events;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitRepositoryConsumers
{
    public class GitRepositoryCreatedConsumer : IConsumer<GitRepositoryCreatedEvent>
    {
        private readonly AppDbContext _db;

        public GitRepositoryCreatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<GitRepositoryCreatedEvent> context)
        {
            var msg = context.Message;
            if (await _db.GitRepositories.FindAsync(msg.Id) is not null)
                return;
            var repository = new GitRepositoryReadModel
            {
                Id = msg.Id,
                Name = msg.Name,
                ExtRepositoryId = msg.ExtRepositoryId,
                WebUrl = msg.WebUrl
            };
            _db.GitRepositories.Add(repository);
            await _db.SaveChangesAsync();
        }
    }
}