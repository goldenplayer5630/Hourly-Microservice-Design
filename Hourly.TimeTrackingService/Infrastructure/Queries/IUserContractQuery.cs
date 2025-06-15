using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Abstractions.Queries
{
    public interface IUserContractQuery
    {
        Task<UserContractReadModel?> GetById(Guid contractId);
        Task<bool> Exists(Guid contractId);
    }
}
