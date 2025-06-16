using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid? UserContractId { get; init; }

        [Required]
        public string TaskDescription { get; init; } = string.Empty;

        [Required]
        public DateTime StartTime { get; init; }

        [Required]
        public DateTime EndTime { get; init; }

        public float BreakTime { get; init; }

        [Required]
        public float Factor { get; init; }

        [Required]
        public float RawEffectiveHours { get; init; }

        [Required]
        public float NetEffectiveHours { get; init; }

        public bool WBSO { get; init; }

        public bool Locked { get; init; }

        public string? OtherRemarks { get; init; }

        public float TVTAccruedHours { get; init; }

        public float TVTUsedHours { get; init; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}