using Hourly.Shared.Events;
using Hourly.TimeTrackingService.Domain.Entities;
using MassTransit;

namespace Hourly.TimeTrackingService.Application.Publishers
{
    public class WorkSessionEventPublisher : IWorkSessionEventPublisher
    {
        private readonly IPublishEndpoint _publish;
        public WorkSessionEventPublisher(IPublishEndpoint publish)
        {
            _publish = publish;
        }
        public async Task PublishWorkSessionCreated(WorkSession workSession)
        {
            await _publish.Publish(new WorkSessionCreatedEvent(workSession.Id, workSession.UserContractId));
        }
        public async Task PublishWorkSessionUpdated(WorkSession workSession)
        {
            await _publish.Publish(new WorkSessionUpdatedEvent(workSession.Id, workSession.UserContractId));
        }
        public async Task PublishWorkSessionDeleted(Guid workSessionId)
        {
            await _publish.Publish(new WorkSessionDeletedEvent(workSessionId));
        }
    }
}
