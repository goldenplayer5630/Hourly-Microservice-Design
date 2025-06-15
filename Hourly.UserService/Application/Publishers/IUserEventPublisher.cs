using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Publishers
{
    public interface IUserEventPublisher
    {
        Task PublishUserCreated(User user);
        Task PublishUserUpdated(User user);
        Task PublishUserDeleted(Guid userId);
    }
}
