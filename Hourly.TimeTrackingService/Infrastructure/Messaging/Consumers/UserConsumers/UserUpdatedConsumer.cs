using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.Shared.Events;
using MassTransit;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Infrastructure.Messaging.Consumers.UserConsumers
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
                    TVTHourBalance = msg.TVTHourBalance
                };
                _db.Users.Add(user);
            } else
            {
                user.Name = msg.Name;
                user.TVTHourBalance = msg.TVTHourBalance;

                _db.Users.Update(user);
            }

            await _db.SaveChangesAsync();
        }
    }
}
