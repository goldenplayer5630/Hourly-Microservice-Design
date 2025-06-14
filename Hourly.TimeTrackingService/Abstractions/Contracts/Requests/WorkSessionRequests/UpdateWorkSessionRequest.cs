using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Abstractions.Contracts.Requests.WorkSessionRequests
{
    public class UpdateWorkSessionRequest
    {
        [Required]
        public Guid UserContractId { get; init; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public float BreakTime { get; set; }

        [Required]
        public float Factor { get; set; }

        public bool WBSO { get; set; }

        public string? OtherRemarks { get; set; }

        public float TVTAccruedHours { get; set; }

        public float TVTUsedHours { get; set; }

        public List<Guid> GitCommitIds { get; set; } = new List<Guid>();
    }
}
