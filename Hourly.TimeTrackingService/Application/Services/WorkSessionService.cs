using Hourly.Shared.Exceptions;
using Hourly.TimeTrackingService.Abstractions.Queries;
using Hourly.TimeTrackingService.Abstractions.Repositories;
using Hourly.TimeTrackingService.Abstractions.Services;
using Hourly.TimeTrackingService.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hourly.TimeTrackingService.Application.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IWorkSessionRepository _repository;
        private readonly IGitCommitQuery _gitCommitQuery;
        private readonly IUserContractQuery _userContractQuery;

        public WorkSessionService(IWorkSessionRepository repository, IUserContractQuery userContractQuery, IGitCommitQuery gitCommitQuery)
        {
            _repository = repository;
            _userContractQuery = userContractQuery;
            _gitCommitQuery = gitCommitQuery;
        }

        public async Task<WorkSession> GetById(Guid workSessionId)
        {
            return await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");
        }

        public async Task<IEnumerable<WorkSession>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<WorkSession>> Filter(Guid? userContractId, int? year, int? month, bool? wbso)
        {
            return await _repository.Filter(userContractId, year, month, wbso);
        }

        public async Task<WorkSession> Create(WorkSession workSession, IEnumerable<Guid> gitCommitIds)
        {
            workSession.Id = Guid.NewGuid();
            workSession.CreatedAt = DateTime.UtcNow;
            workSession.Validate();

            var userContract = await _userContractQuery.GetByIdAsync(workSession.UserContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");

            if (workSession.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");


            var commits = await _gitCommitQuery.GetByIdsAsync(gitCommitIds);
            if (commits.Count != gitCommitIds.Count())
            {
                var missing = gitCommitIds.Except(commits.Select(c => c.Id));
                throw new EntityNotFoundException($"Missing GitCommits: {string.Join(", ", missing)}");
            }

            workSession.AssignToUserContract(userContract);

            return await _repository.Create(workSession);
        }

        public async Task<WorkSession> Update(WorkSession updated, IEnumerable<Guid> gitCommitIds)
        {
            var existing = await _repository.GetById(updated.Id)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            var userContract = await _userContractQuery.GetByIdAsync(updated.UserContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");

            existing.Update(updated);

            if (existing.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");

            existing.AssignToUserContract(userContract);

            // Replace commit links
            existing.GitCommits.Clear();

            var commits = await _gitCommitQuery.GetByIdsAsync(gitCommitIds);
            if (commits.Count != gitCommitIds.Count())
            {
                var missing = gitCommitIds.Except(commits.Select(c => c.Id));
                throw new EntityNotFoundException($"Missing GitCommits: {string.Join(", ", missing)}");
            }

            return await _repository.Update(existing);
        }

        public async Task<WorkSession> UpdateLock(Guid workSessionId, bool locked)
        {
            var existing = await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            existing.Locked = locked;

            existing.UpdatedAt = DateTime.UtcNow;
            return await _repository.Update(existing);
        }

        public async Task<WorkSession> AddGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            return await AddGitCommit(workSession, gitCommitId);
        }

        private async Task<WorkSession> AddGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitQuery.GetById(gitCommitId)
                ?? throw new EntityNotFoundException("GitCommit not found in WorkSession!");

            workSession.AddGitCommit(gitCommit);

            await _repository.SaveChanges();

            return workSession;
        }

        public async Task<WorkSession> RemoveGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            return await RemoveGitCommit(workSession, gitCommitId);
        }

        private async Task<WorkSession> RemoveGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitQuery.GetById(gitCommitId)
                ?? throw new EntityNotFoundException("GitCommit not found in WorkSession!");

            workSession.RemoveGitCommit(gitCommit);

            await _repository.SaveChanges();

            return workSession;
        }

        public async Task Delete(Guid workSessionId)
        {
            var existing = await _repository.GetById(workSessionId);
            if (existing == null)
            {
                throw new EntityNotFoundException("WorkSession not found!");
            }

            existing.Validate();

            await _repository.Delete(workSessionId);
        }
    }
}
