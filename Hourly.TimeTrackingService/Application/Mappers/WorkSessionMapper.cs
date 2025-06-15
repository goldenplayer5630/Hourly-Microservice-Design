using Hourly.TimeTrackingService.Application.Mappers;
using Hourly.TimeTrackingService.Contracts.Requests.WorkSessionRequests;
using Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses;
using Hourly.TimeTrackingService.Domain.Entities;

namespace Hourly.Domain.Mappers
{
    public static partial class WorkSessionMapper
    {
        public static WorkSessionResponse ToResponse(this WorkSession entity)
        {
            return new WorkSessionResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
                UserContract = entity.UserContract.ToSummaryResponse(),
                TaskDescription = entity.TaskDescription,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Factor = entity.Factor,
                BreakTime = entity.BreakTime,
                RawEffectiveHours = entity.RawEffectiveHours,
                NetEffectiveHours = entity.NetEffectiveHours,
                WBSO = entity.WBSO,
                Locked = entity.Locked,
                OtherRemarks = entity.OtherRemarks,
                GitCommits = entity.GitCommits.Select(gc => gc.ToResponse()).ToList(),
                TVTAccruedHours = entity.TVTAccruedHours,
                TVTUsedHours = entity.TVTUsedHours,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static WorkSessionSummaryResponse ToSummaryResponse(this WorkSession entity)
        {
            return new WorkSessionSummaryResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
                TaskDescription = entity.TaskDescription,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Factor = entity.Factor,
                BreakTime = entity.BreakTime,
                RawEffectiveHours = entity.RawEffectiveHours,
                NetEffectiveHours = entity.NetEffectiveHours,
                WBSO = entity.WBSO,
                Locked = entity.Locked,
                OtherRemarks = entity.OtherRemarks,
                TVTAccruedHours = entity.TVTAccruedHours,
                TVTUsedHours = entity.TVTUsedHours,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static WorkSession ToWorkSession(this CreateWorkSessionRequest request)
        {
            return new WorkSession
            {
                TaskDescription = request.TaskDescription,
                UserContractId = request.UserContractId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Factor = request.Factor,
                BreakTime = request.BreakTime,
                WBSO = request.WBSO,
                OtherRemarks = request.OtherRemarks,
                TVTAccruedHours = request.TVTAccruedHours,
                TVTUsedHours = request.TVTUsedHours,
            };
        }

        public static WorkSession ToWorkSession(this UpdateWorkSessionRequest request, Guid id)
        {
            return new WorkSession
            {
                Id = id,
                TaskDescription = request.TaskDescription,
                UserContractId = request.UserContractId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Factor = request.Factor,
                BreakTime = request.BreakTime,
                WBSO = request.WBSO,
                OtherRemarks = request.OtherRemarks,
                TVTAccruedHours = request.TVTAccruedHours,
                TVTUsedHours = request.TVTUsedHours,
            };
        }
    }
}
