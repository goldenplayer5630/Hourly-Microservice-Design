using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Responses.UserContractResponses
{
    public class UserContractSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }
        [Required]
        public Guid UserId { get; init; }
        [Required]
        public string Name { get; init; } = string.Empty;
        [Required]
        public ContractType ContractType { get; init; }
        public bool IsActive { get; init; }

        // VINCENT TODO: Change this to weekly, add a monthly avarage
        // VINCENT TODO: Make sure that the minimum amount of hours is the maximum amount of TVT hours
        [Required]
        public double MinWeeklyHours { get; init; }
        [Required]
        public double MaxWeeklyHours { get; init; }

        public double? GrossHourlyRate { get; init; }
        public int? HolidayHoursPercentage { get; init; }
        public bool MonthlyPaidHolidayHours { get; init; }

        public double MinimumHoursPerMonth { get; init; }
        public double MaximumHoursPerMonth { get; init; }

        [Required]
        public DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public string? ContractFilePath { get; init; }
        public string? Description { get; init; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
