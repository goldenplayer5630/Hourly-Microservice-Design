using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.Shared.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserConsumers
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
            
            var userContracts = await _db.UserContracts
                .Where(uc => uc.UserId == msg.Id)
                .ToListAsync();

            if (userContracts is not null && userContracts.Any())
            {
                _db.UserContracts.RemoveRange(userContracts);
            }

            await _db.SaveChangesAsync();
        }
    }
}
