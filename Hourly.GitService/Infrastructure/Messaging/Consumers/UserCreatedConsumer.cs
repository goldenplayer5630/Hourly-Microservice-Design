using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.GitService.Infrastructure.Messaging.Consumers
{
    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        private readonly AppDbContext _db;

        public UserCreatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            var msg = context.Message;

            if (await _db.Users.FindAsync(msg.Id) is not null)
                return;

            var user = new UserReadModel
            {
                Id = msg.Id,
                Name = msg.Name,
                GitEmail = msg.GitEmail,
                GitAccessToken = msg.GitAccessToken,
                GitUsername = msg.GitUserName
            };
            _db.Users.Add(user);

            await _db.SaveChangesAsync();
        }
    }
}