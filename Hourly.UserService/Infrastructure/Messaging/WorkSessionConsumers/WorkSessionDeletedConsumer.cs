using Hourly.UserService.Abstractions.Events;
using Hourly.UserService.Infrastructure.Persistence;
using MassTransit;

namespace Hourly.UserService.Infrastructure.Messaging.WorkSessionConsumers
{
    public class WorkSessionDeletedConsumer : IConsumer<WorkSessionDeletedEvent>
    {
        private readonly AppDbContext _db;
        public WorkSessionDeletedConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<WorkSessionDeletedEvent> context)
        {
            var msg = context.Message;
            var ws = await _db.WorkSessions.FindAsync(msg.Id);

            if (ws is null)
                return;

            _db.WorkSessions.Remove(ws);
            await _db.SaveChangesAsync();
        }
    }
}
