using Hourly.TimeTrackingService.Api.Contracts.Requests.GitCommitResponses;
using Hourly.TimeTrackingService.Api.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.WorkSessionResponses
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
