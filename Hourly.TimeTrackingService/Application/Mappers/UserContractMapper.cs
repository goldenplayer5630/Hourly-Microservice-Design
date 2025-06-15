using Hourly.Domain.Mappers;
using Hourly.TimeTrackingService.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Application.Mappers
{
    public static partial class UserContractMapper
    {
        public static UserContractResponse ToResponse(this UserContractReadModel entity)
        {
            return new UserContractResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                User = entity.User.ToSummaryResponse(),
                WorkSessions = entity.WorkSessions.Select(ws => ws.ToSummaryResponse()).ToList()
            };
        }

        public static UserContractSummaryResponse ToSummaryResponse(this UserContractReadModel entity)
        {
            return new UserContractSummaryResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
            };
        }
    }
}
