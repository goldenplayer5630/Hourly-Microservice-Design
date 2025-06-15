using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserContractConsumers
{
    public class UserContractDeletedConsumer : IConsumer<UserContractDeletedEvent>
    {
        private readonly AppDbContext _db;
        public UserContractDeletedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<UserContractDeletedEvent> context)
        {
            var msg = context.Message;
            var userContract = await _db.UserContracts.FindAsync(msg.Id);

            if (userContract is null)
                return;

            _db.UserContracts.Remove(userContract);
            await _db.SaveChangesAsync();
        }
    }
}
