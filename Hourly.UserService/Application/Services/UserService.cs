using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Abstractions.Services;
using Hourly.UserService.Domain.Entities;

namespace Hourly.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository repository, IDepartmentRepository departmentRepository, IRoleRepository roleRepository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _roleRepository = roleRepository;
        }

        public async Task<User> GetById(Guid userId)
        {
            return await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<User> Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            var role = await _roleRepository.GetById(user.RoleId)
                ?? throw new EntityNotFoundException("Role not found!");

            user.AssignToRole(role);

            return await _repository.Create(user);
        }

        public async Task<User> AddDepartment(Guid userId, Guid departmentId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            var department = await _departmentRepository.GetById(departmentId)
                ?? throw new Exception("Department not found!");

            user.AssignToDepartment(department);

            await _repository.SaveChanges();

            return user;
        }

        public async Task<User> RemoveDepartment(Guid userId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            user.RemoveFromDepartment();

            await _repository.SaveChanges();

            return user;
        }

        public async Task<User> Update(User user)
        {
            return await _repository.Update(user);
        }

        public async Task Delete(Guid userId)
        {
            await _repository.Delete(userId);
        }
    }
}
