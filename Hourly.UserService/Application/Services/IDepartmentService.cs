using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(Guid departmentId);
        Task<Department> Create(Department department);
        Task<Department> Update(Department department);
        Task Delete(Guid departmentId);
    }
}
