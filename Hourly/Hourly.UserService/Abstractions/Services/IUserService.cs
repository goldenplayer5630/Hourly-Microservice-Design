using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Abstractions.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid userId);
        Task<User> Create(User user);
        Task<User> AddDepartment(Guid userId, Guid departmentId);
        Task<User> RemoveDepartment(Guid userId);
        Task<User> Update(User user);
        Task Delete(Guid userId);
    }

}

