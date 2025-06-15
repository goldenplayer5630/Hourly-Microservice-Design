using Hourly.Shared.Events;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.GitRepositoryConsumers
{
    public class GitRepositoryUpdatedConsumer : IConsumer<GitRepositoryUpdatedEvent>
    {
        private readonly AppDbContext _db;
        public GitRepositoryUpdatedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<GitRepositoryUpdatedEvent> context)
        {
            var msg = context.Message;
            var repository = await _db.GitRepositories.FindAsync(msg.Id);
            if (repository is null)
            {
                repository = new GitRepositoryReadModel
                {
                    Id = msg.Id,
                    Name = msg.Name,
                    WebUrl = msg.WebUrl,
                    ExtRepositoryId = msg.ExtRepositoryId
                };
                _db.GitRepositories.Add(repository);
            } else
            {
                repository.Name = msg.Name;
                repository.WebUrl = msg.WebUrl;
                repository.ExtRepositoryId = msg.ExtRepositoryId;
                _db.GitRepositories.Update(repository);
            }

            await _db.SaveChangesAsync();
        }
    }
}
