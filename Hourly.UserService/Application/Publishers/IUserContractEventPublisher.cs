using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Publishers
{
    public interface IUserContractEventPublisher
    {
        Task PublishUserContractCreated(UserContract userContract);
        Task PublishUserContractUpdated(UserContract userContract);
        Task PublishUserContractDeleted(Guid userContractId);
    }
}
