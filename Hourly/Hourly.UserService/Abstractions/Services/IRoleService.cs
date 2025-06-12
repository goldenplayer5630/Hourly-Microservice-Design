using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Abstractions.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(Guid roleId);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
    }
}
