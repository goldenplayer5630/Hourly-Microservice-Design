using Hourly.Shared.Events;
using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using MassTransit;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserContractConsumers
{
    public class UserContractUpdatedConsumer
    {
        private readonly AppDbContext _db;

        public UserContractUpdatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<UserContractUpdatedEvent> context)
        {
            var msg = context.Message;
            var userContract = await _db.UserContracts.FindAsync(msg.Id);

            if (userContract is null)
            {
                userContract = new UserContractReadModel()
                {
                    Id = msg.Id,
                    Name = msg.Name,
                    UserId = msg.UserId
                };

                _db.UserContracts.Add(userContract);
            } else
            {
                userContract.Name = msg.Name;
                userContract.UserId = msg.UserId;
                _db.UserContracts.Update(userContract);
            }

            await _db.SaveChangesAsync();
        }
    }
}
