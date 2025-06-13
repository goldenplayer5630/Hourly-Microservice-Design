using Hourly.UserService.Api.Contracts.Requests.UserContractRequests;
using Hourly.UserService.Api.Contracts.Responses.UserContractResponses;
using Hourly.UserService.Domain.Entities;

namespace Hourly.UserService.Application.Mappers
{
    public static partial class UserContractMapper
    {
        public static UserContract ToUserContract(this CreateUserContractRequest request)
        {
            return new UserContract
            {
                UserId = request.UserId,
                Name = request.Name,
                ContractType = request.ContractType,
                MinWeeklyHours = request.MinWeeklyHours,
                MaxWeeklyHours = request.MaxWeeklyHours,
                IsActive = request.IsActive,
                GrossHourlyRate = request.GrossHourlyRate,
                HolidayHoursPercentage = request.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = request.MonthlyPaidHolidayHours,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContractFilePath = request.ContractFilePath,
                Description = request.Description,
            };
        }

        public static UserContract ToUserContract(this UpdateUserContractRequest request, Guid id)
        {
            return new UserContract
            {
                Id = id,
                UserId = request.UserId,
                Name = request.Name,
                ContractType = request.ContractType,
                MinWeeklyHours = request.MinWeeklyHours,
                MaxWeeklyHours = request.MaxWeeklyHours,
                IsActive = request.IsActive,
                GrossHourlyRate = request.GrossHourlyRate,
                HolidayHoursPercentage = request.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = request.MonthlyPaidHolidayHours,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContractFilePath = request.ContractFilePath,
                Description = request.Description
            };
        }

        public static UserContractResponse ToResponse(this UserContract entity)
        {
            return new UserContractResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                ContractType = entity.ContractType,
                MinWeeklyHours = entity.MinWeeklyHours,
                MaxWeeklyHours = entity.MaxWeeklyHours,
                GrossHourlyRate = entity.GrossHourlyRate,
                IsActive = entity.IsActive,
                MinimumHoursPerMonth = entity.MinimumHoursPerMonth,
                MaximumHoursPerMonth = entity.MaximumHoursPerMonth,
                HolidayHoursPercentage = entity.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = entity.MonthlyPaidHolidayHours,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ContractFilePath = entity.ContractFilePath,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                User = entity.User.ToSummaryResponse(),
                WorkSessions = entity.WorkSessions.Select(ws => ws.ToSummaryResponse()).ToList()
            };
        }

        public static UserContractSummaryResponse ToSummaryResponse(this UserContract entity)
        {
            return new UserContractSummaryResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                ContractType = entity.ContractType,
                MinWeeklyHours = entity.MinWeeklyHours,
                MaxWeeklyHours = entity.MaxWeeklyHours,
                GrossHourlyRate = entity.GrossHourlyRate,
                IsActive = entity.IsActive,
                MinimumHoursPerMonth = entity.MinimumHoursPerMonth,
                MaximumHoursPerMonth = entity.MaximumHoursPerMonth,
                HolidayHoursPercentage = entity.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = entity.MonthlyPaidHolidayHours,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ContractFilePath = entity.ContractFilePath,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
