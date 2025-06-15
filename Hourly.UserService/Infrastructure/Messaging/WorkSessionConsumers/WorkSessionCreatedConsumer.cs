using Hourly.Shared.Events;
using Hourly.UserService.Infrastructure.Persistence;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using MassTransit;

namespace Hourly.UserService.Infrastructure.Messaging.WorkSessionConsumers
{
    public class WorkSessionCreatedConsumer : IConsumer<WorkSessionCreatedEvent>
    {

        private readonly AppDbContext _db;

        public WorkSessionCreatedConsumer(AppDbContext db)
        {
            _db = db;
        }

        public async Task Consume(ConsumeContext<WorkSessionCreatedEvent> context)
        {
            var msg = context.Message;

            if (await _db.WorkSessions.FindAsync(msg.Id) is not null)
                return;

            var ws = new WorkSessionReadModel()
            {
                Id = msg.Id,
                UserContractId = msg.UserContractId,
            };

            _db.WorkSessions.Add(ws);
            await _db.SaveChangesAsync();
        }
    }
}
