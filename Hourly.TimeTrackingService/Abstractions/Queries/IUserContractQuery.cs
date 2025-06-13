using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Abstractions.Queries
{
    public interface IUserContractQuery
    {
        Task<UserContractReadModel?> GetByIdAsync(Guid contractId);
        Task<bool> ExistsAsync(Guid contractId);
    }
}
