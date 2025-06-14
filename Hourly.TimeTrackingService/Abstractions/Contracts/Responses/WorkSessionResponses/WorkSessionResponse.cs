using Hourly.TimeTrackingService.Abstractions.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Abstractions.Contracts.Responses.GitCommitResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Abstractions.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {

        [ForeignKey("UserContractId")]
        public UserContractSummaryResponse? UserContract { get; set; }

        public ICollection<GitCommitResponse> GitCommits { get; set; } = new List<GitCommitResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
