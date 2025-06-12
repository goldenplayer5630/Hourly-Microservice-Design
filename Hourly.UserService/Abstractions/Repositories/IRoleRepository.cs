using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Abstractions.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetById(Guid roleId);
        Task<IEnumerable<Role>> GetAll();
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task SaveChanges();
    }
}
