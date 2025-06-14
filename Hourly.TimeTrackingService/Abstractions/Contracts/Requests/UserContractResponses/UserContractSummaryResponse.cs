using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.UserContractResponses
{
    public class UserContractSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ContractType ContractType { get; set; }
        public bool IsActive { get; set; }

        // VINCENT TODO: Change this to weekly, add a monthly avarage
        // VINCENT TODO: Make sure that the minimum amount of hours is the maximum amount of TVT hours
        [Required]
        public double MinWeeklyHours { get; set; }
        [Required]
        public double MaxWeeklyHours { get; set; }

        public double? GrossHourlyRate { get; set; }
        public int? HolidayHoursPercentage { get; set; }
        public bool MonthlyPaidHolidayHours { get; set; }

        public double MinimumHoursPerMonth { get; set; }
        public double MaximumHoursPerMonth { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ContractFilePath { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
