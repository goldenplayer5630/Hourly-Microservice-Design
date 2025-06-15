using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetById(Guid userId);
        Task<IEnumerable<User>> GetAll();
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task Delete(Guid userId);
        Task SaveChanges();
    }
}
