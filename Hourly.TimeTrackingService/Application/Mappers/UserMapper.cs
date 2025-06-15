using Hourly.TimeTrackingService.Contracts.Responses.UserResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;

namespace Hourly.TimeTrackingService.Application.Mappers
{
    public static partial class UserMapper
    {
        public static UserResponse ToResponse(this UserReadModel entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                TVTHourBalance = entity.TVTHourBalance,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
                Contracts = entity.Contracts.Select(uc => uc.ToSummaryResponse()).ToList(),
            };
        }

        public static UserSummaryResponse ToSummaryResponse(this UserReadModel entity)
        {
            return new UserSummaryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}
