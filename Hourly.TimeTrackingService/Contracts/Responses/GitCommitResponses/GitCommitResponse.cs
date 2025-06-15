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
        public UserSummaryResponse Author { get; set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepositorySummaryResponse Repository { get; internal set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}
