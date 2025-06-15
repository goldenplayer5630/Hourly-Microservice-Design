using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Infrastructure.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetById(Guid departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task<Department> Create(Department department);
        Task<Department> Update(Department department);
        Task Delete(Guid departmentId);
        Task SaveChanges();
    }
}
