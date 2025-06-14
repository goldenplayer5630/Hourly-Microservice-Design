using Hourly.UserService.Abstractions.Events;
using Hourly.UserService.Abstractions.Publishers;
using Hourly.UserService.Domain.Entities;
using MassTransit;

namespace Hourly.UserService.Application.Publishers
{
    public class UserEventPublisher : IUserEventPublisher
    {
        private readonly IPublishEndpoint _publish;

        public UserEventPublisher(IPublishEndpoint publish)
        {
            _publish = publish;
        }

        public async Task PublishUserCreated(User user)
        {
            await _publish.Publish(new UserCreatedEvent(user.Id, user.Name, user.Email, user.TVTHourBalance, user.GitEmail, user.GitUsername, user.GitAccessToken));
        }

        public async Task PublishUserUpdated(User user)
        {
            await _publish.Publish(new UserUpdatedEvent(user.Id, user.Name, user.Email, user.TVTHourBalance, user.GitEmail, user.GitUsername, user.GitAccessToken));
        }

        public async Task PublishUserDeleted(Guid userId)
        {
            await _publish.Publish(new UserDeletedEvent(userId));
        }
    }
}
