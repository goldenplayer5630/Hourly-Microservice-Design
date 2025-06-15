using Hourly.Shared.Events;
using Hourly.UserService.Domain.Entities;
using MassTransit;

namespace Hourly.UserService.Application.Publishers
{
    public class UserContractEventPublisher : IUserContractEventPublisher
    {
        private readonly IPublishEndpoint _publish;
        public UserContractEventPublisher(IPublishEndpoint publish)
        {
            _publish = publish;
        }
        public async Task PublishUserContractCreated(UserContract userContract)
        {
            await _publish.Publish(new UserContractCreatedEvent(userContract.Id, userContract.UserId, userContract.Name));
        }
        public async Task PublishUserContractUpdated(UserContract userContract)
        {
            await _publish.Publish(new UserContractUpdatedEvent(userContract.Id, userContract.UserId, userContract.Name));
        }
        public async Task PublishUserContractDeleted(Guid userContractId)
        {
            await _publish.Publish(new UserContractDeletedEvent(userContractId));
        }
    }
}
