using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Requests.WorkSessionRequests
{
    public class CreateWorkSessionRequest
    {
        [Required]
        public Guid UserContractId { get; init; }

        [Required]
        public string TaskDescription { get; init; } = string.Empty;

        [Required]
        public DateTime StartTime { get; init; }

        [Required]
        public DateTime EndTime { get; init; }

        public float BreakTime { get; init; }

        [Required]
        public float Factor { get; init; }

        public bool WBSO { get; init; }

        public string? OtherRemarks { get; init; }

        public float TVTAccruedHours { get; init; }

        public float TVTUsedHours { get; init; }

        public List<Guid> GitCommitIds { get; init; } = new List<Guid>();
    }
}
