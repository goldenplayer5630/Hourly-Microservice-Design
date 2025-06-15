using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Infrastructure.Queries
{
    public interface IUserContractQuery
    {
        Task<UserContractReadModel?> GetById(Guid contractId);
        Task<bool> Exists(Guid contractId);
    }
}
