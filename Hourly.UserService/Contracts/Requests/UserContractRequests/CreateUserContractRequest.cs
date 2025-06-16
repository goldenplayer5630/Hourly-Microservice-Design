using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Requests.UserContractRequests
{
    public class CreateUserContractRequest
    {
        [Required]
        public Guid UserId { get; init; }
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public ContractType ContractType { get; init; }
        public bool IsActive { get; init; }

        [Required]
        public double MinWeeklyHours { get; init; }
        [Required]
        public double MaxWeeklyHours { get; init; }

        public float? GrossHourlyRate { get; init; }
        public int? HolidayHoursPercentage { get; init; }
        public bool MonthlyPaidHolidayHours { get; init; }

        [Required]
        public DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? ContractFilePath { get; init; }
        public string? Description { get; init; }
    }
}
