using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Api.Contracts.Requests.UserContractRequests
{
    public class UpdateUserContractRequest
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ContractType ContractType { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public double MinWeeklyHours { get; set; }
        [Required]
        public double MaxWeeklyHours { get; set; }

        public float? GrossHourlyRate { get; set; }
        public int? HolidayHoursPercentage { get; set; }
        public bool MonthlyPaidHolidayHours { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ContractFilePath { get; set; }
        public string? Description { get; set; }
    }
}
