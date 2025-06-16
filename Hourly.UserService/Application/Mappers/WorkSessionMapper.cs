using Hourly.UserService.Contracts.Responses.WorkSessionResponses;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class WorkSessionMapper
    {
        public static WorkSessionResponse ToResponse(this WorkSessionReadModel entity)
        {
            return new WorkSessionResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
                UserContract = entity.UserContract?.ToSummaryResponse()
            };
        }

        public static WorkSessionSummaryResponse ToSummaryResponse(this WorkSessionReadModel entity)
        {
            return new WorkSessionSummaryResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
            };
        }
    }
}
