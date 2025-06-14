using Hourly.TimeTrackingService.Api.Contracts.Requests.GitRepositoryResponses;
using Hourly.TimeTrackingService.Api.Contracts.Requests.UserResponses;
using Hourly.TimeTrackingService.Api.Contracts.Requests.WorkSessionResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Api.Contracts.Requests.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {
        [ForeignKey("AuthorId")]
        public UserSummaryResponse Author { get; set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepositorySummaryResponse Repository { get; internal set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}
