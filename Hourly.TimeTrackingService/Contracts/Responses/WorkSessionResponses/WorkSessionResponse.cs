using Hourly.TimeTrackingService.Contracts.Requests.UserContractResponses;
using Hourly.TimeTrackingService.Contracts.Responses.GitCommitResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {

        [ForeignKey("UserContractId")]
        public UserContractSummaryResponse? UserContract { get; init; }

        public ICollection<GitCommitResponse> GitCommits { get; init; } = new List<GitCommitResponse>();
    }
}
