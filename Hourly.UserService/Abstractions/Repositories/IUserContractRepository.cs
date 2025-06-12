using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Abstractions.Repositories
{
    public interface IUserContractRepository
    {
        Task<UserContract?> GetById(Guid userContractId);
        Task<IEnumerable<UserContract>> FilterUserContracts(Guid? userId, int? year, int? month, bool? isActive);
        Task<IEnumerable<UserContract>> GetAll();
        Task<UserContract> Create(UserContract userContract);
        Task<UserContract> Update(UserContract userContract);
        Task Delete(Guid userContractId);
        Task SaveChanges();
    }
}
