using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Abstractions.Services;
using Hourly.UserService.Domain.Entities;

namespace Hourly.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Department> GetById(Guid departmentId)
        {
            return await _repository.GetById(departmentId)
                ?? throw new EntityNotFoundException("Department not found!");
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Department> Create(Department department)
        {
            department.Id = Guid.NewGuid();
            department.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(department);
        }

        public async Task<Department> Update(Department department)
        {
            department.UpdatedAt = DateTime.UtcNow;
            return await _repository.Update(department);
        }

        public async Task Delete(Guid departmentId)
        {
            await _repository.Delete(departmentId);
        }
    }
}
