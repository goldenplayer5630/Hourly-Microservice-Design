using Hourly.TimeTrackingService.Contracts.Responses.GitRepositoryResponses;
using Hourly.TimeTrackingService.Contracts.Responses.UserResponses;
using Hourly.TimeTrackingService.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {
        [ForeignKey("AuthorId")]
        public UserSummaryResponse? Author { get; init; }

        [Required]
        [ForeignKey("GitRepositoryId")]
        public GitRepositorySummaryResponse? GitRepository { get; init; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; init; } = new List<WorkSessionSummaryResponse>();
    }
}
