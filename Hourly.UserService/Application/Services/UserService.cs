using Hourly.Shared.Exceptions;
using Hourly.UserService.Application.Publishers;
using Hourly.UserService.Application.Services;
using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Repositories;

namespace Hourly.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUserEventPublisher _userEventPublisher;

        public UserService(IUserRepository repository, IDepartmentRepository departmentRepository, IUserEventPublisher userEventPublisher)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _userEventPublisher = userEventPublisher;
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

            var result = await _repository.Create(user);
            await _userEventPublisher.PublishUserCreated(result);
            return result;
        }

        public async Task<User> AddDepartment(Guid userId, Guid departmentId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            var department = await _departmentRepository.GetById(departmentId)
                ?? throw new Exception("Department not found!");

            user.AssignToDepartment(department);

            await _repository.Update(user);

            return user;
        }

        public async Task<User> RemoveDepartment(Guid userId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            user.RemoveFromDepartment();

            await _repository.Update(user);

            return user;
        }

        public async Task<User> Update(User user)
        {
            var existing = await _repository.GetById(user.Id)
                ?? throw new EntityNotFoundException("User not found!");

            existing.Update(user);

            var result = await _repository.Update(existing);
            await _userEventPublisher.PublishUserUpdated(result);
            return result;
        }

        public async Task Delete(Guid userId)
        {
            await _repository.Delete(userId);

            await _userEventPublisher.PublishUserDeleted(userId);
        }
    }
}
