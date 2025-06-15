using Hourly.Shared.Events;
using Hourly.UserService.Infrastructure.Persistence;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using MassTransit;

namespace Hourly.UserService.Infrastructure.Messaging.WorkSessionConsumers
{
    public class WorkSessionUpdatedConsumer : IConsumer<WorkSessionUpdatedEvent>
    {
        private readonly AppDbContext _db;
        public WorkSessionUpdatedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<WorkSessionUpdatedEvent> context)
        {
            var msg = context.Message;
            var ws = await _db.WorkSessions.FindAsync(msg.Id);

            if (ws is null)
            {
                ws = new WorkSessionReadModel
                {
                    Id = msg.Id,
                    UserContractId = msg.UserContractId
                };
                _db.WorkSessions.Add(ws);
            } else
            {
                ws.UserContractId = msg.UserContractId;
                _db.WorkSessions.Update(ws);
            }

            await _db.SaveChangesAsync();
        }
    }
}
