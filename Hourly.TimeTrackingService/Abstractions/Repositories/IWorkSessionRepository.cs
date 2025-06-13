using Hourly.TimeTrackingService.Domain.Entities;

namespace Hourly.TimeTrackingService.Abstractions.Repositories
{
    public interface IWorkSessionRepository
    {
        Task<WorkSession?> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> GetAll();
        Task<IEnumerable<WorkSession>> Filter(Guid? userContractId, int? year, int? month, bool? wbso);
        Task<WorkSession> Create(WorkSession workSession);
        Task<WorkSession> Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
        Task SaveChanges();
    }
}
