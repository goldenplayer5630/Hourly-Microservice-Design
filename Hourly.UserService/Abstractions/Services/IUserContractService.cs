using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Abstractions.Services
{
    public interface IUserContractService
    {
        Task<IEnumerable<UserContract>> GetAll();
        Task<IEnumerable<UserContract>> FilterUserContracts(Guid? userId, int? year, int? month, bool? isActive);
        Task<UserContract> GetById(Guid userContractId);
        Task<UserContract> Create(UserContract userContract);
        Task<UserContract> Update(UserContract userContract);
        Task Delete(Guid userContractId);
    }
}
