using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserContractId { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public float BreakTime { get; set; }

        [Required]
        public float Factor { get; set; }

        [Required]
        public float RawEffectiveHours { get; set; }

        [Required]
        public float NetEffectiveHours { get; set; }

        public bool WBSO { get; set; }

        public bool Locked { get; set; }

        public string? OtherRemarks { get; set; }

        public float TVTAccruedHours { get; set; }

        public float TVTUsedHours { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}