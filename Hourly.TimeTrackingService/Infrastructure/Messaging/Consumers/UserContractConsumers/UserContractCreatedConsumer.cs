using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Events;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserContractConsumers
{
    public class UserContractCreatedConsumer : IConsumer<UserContractCreatedEvent> 
    {
        private readonly AppDbContext _db;

        public UserContractCreatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<UserContractCreatedEvent> context)
        {
            var msg = context.Message;
            if (await _db.UserContracts.FindAsync(msg.Id) is not null)
                return;
            var userContract = new UserContractReadModel
            {
                Id = msg.Id,
                UserId = msg.UserId,
                Name = msg.Name,
            };
            _db.UserContracts.Add(userContract);
            await _db.SaveChangesAsync();
        }
    }
}
