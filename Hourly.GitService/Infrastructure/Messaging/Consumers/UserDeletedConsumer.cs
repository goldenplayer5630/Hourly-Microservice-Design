using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.GitService.Infrastructure.Messaging.Consumers
{
    public class UserDeletedConsumer : IConsumer<UserDeletedEvent>
    {
        private readonly AppDbContext _db;

        public UserDeletedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<UserDeletedEvent> context)
        {
            var msg = context.Message;
            var user = await _db.Users.FindAsync(msg.Id);
            if (user is null)
                return;

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
