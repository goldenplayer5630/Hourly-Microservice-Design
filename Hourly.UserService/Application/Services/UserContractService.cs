using Hourly.Shared.Exceptions;
using Hourly.UserService.Application.Publishers;
using Hourly.UserService.Application.Services;
using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Repositories;
using MassTransit;

namespace Hourly.Application.Services
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUserContractEventPublisher _userContractEventPublisher;

        public UserContractService(IUserContractRepository repository, IUserRepository userRepository, IUserContractEventPublisher userContractEventPublisher)
        {
            _repository = repository;
            _userRepository = userRepository;
            _userContractEventPublisher = userContractEventPublisher;
        }

        public async Task<UserContract> GetById(Guid userContractId)
        {
            return await _repository.GetById(userContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");
        }

        public async Task<IEnumerable<UserContract>> FilterUserContracts(Guid? userId, int? year, int? month, bool? isActive)
        {
            return await _repository.FilterUserContracts(userId, year, month, isActive);
        }

        public async Task<IEnumerable<UserContract>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserContract> Create(UserContract userContract)
        {
            userContract.Id = Guid.NewGuid();
            userContract.CreatedAt = DateTime.UtcNow;

            userContract.Validate();

            var user = await _userRepository.GetById(userContract.UserId)
                ?? throw new EntityNotFoundException("User not found!");

            var activeContracts = await _repository.FilterUserContracts(user.Id, null, null, true);

            if (activeContracts.Any() && userContract.IsActive)
            {
                throw new DomainValidationException("User already has an active contract.");
            }

            userContract.AssignToUser(user);

            var result = await _repository.Create(userContract);

            await _userContractEventPublisher.PublishUserContractCreated(result);

            return result;
        }

        public async Task<UserContract> Update(UserContract userContract)
        {
            var existing = await _repository.GetById(userContract.Id)
                ?? throw new EntityNotFoundException("UserContract not found!");

            var user = await _userRepository.GetById(userContract.UserId)
                ?? throw new EntityNotFoundException("User not found!");

            var activeContracts = await _repository.FilterUserContracts(user.Id, null, null, true);

            if (activeContracts.Any() && userContract.IsActive)
            {
                if (activeContracts.FirstOrDefault()?.Id != userContract.Id)
                {
                    throw new DomainValidationException("User already has an active contract.");
                }
            }

            existing.Update(userContract);

            existing.AssignToUser(user);

            var result = await _repository.Update(existing);

            await _userContractEventPublisher.PublishUserContractUpdated(result);

            return result;
        }

        public async Task Delete(Guid userContractId)
        {
            await _repository.Delete(userContractId);

            await _userContractEventPublisher.PublishUserContractDeleted(userContractId);
        }
    }
}