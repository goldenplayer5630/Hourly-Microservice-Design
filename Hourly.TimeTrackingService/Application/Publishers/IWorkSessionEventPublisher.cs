using Hourly.TimeTrackingService.Domain.Entities;

namespace Hourly.TimeTrackingService.Application.Publishers
{
    public interface IWorkSessionEventPublisher
    {
        Task PublishWorkSessionCreated(WorkSession WorkSession);
        Task PublishWorkSessionUpdated(WorkSession WorkSession);
        Task PublishWorkSessionDeleted(Guid WorkSessionId);
    }
}
