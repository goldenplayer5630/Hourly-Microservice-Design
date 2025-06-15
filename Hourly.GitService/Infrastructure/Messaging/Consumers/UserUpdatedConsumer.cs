using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.GitService.Infrastructure.Messaging.Consumers
{
    public class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
    {
        private readonly AppDbContext _db;

        public UserUpdatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
        {
            var msg = context.Message;
            var user = await _db.Users.FindAsync(msg.Id);

            if (user is null)
            {
                user = new UserReadModel()
                {
                    Id = msg.Id,
                    Name = msg.Name,
                    GitAccessToken = msg.GitAccessToken,
                    GitEmail = msg.GitEmail,
                    GitUsername = msg.GitUserName
                };
                _db.Users.Add(user);
            } else
            {
                user.Name = msg.Name;
                user.GitAccessToken = msg.GitAccessToken;
                user.GitEmail = msg.GitEmail;
                user.GitUsername = msg.GitUserName;

                _db.Users.Update(user);
            }

            await _db.SaveChangesAsync();
        }   
    }
}