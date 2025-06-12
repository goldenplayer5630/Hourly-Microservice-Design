using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Abstractions.Services;
using Hourly.UserService.Domain.Entities;

namespace Hourly.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Role> GetById(Guid roleId)
        {
            return await _repository.GetById(roleId)
                ?? throw new EntityNotFoundException("Role not found!");
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Role> Create(Role role)
        {
            role.Id = Guid.NewGuid();
            role.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(role);
        }

        public async Task<Role> Update(Role role)
        {
            role.UpdatedAt = DateTime.UtcNow;
            return await _repository.Update(role);
        }
    }
}
