using Hourly.GitService.Contracts.Responses.GitRepositoryResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.GitService.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {

        [Required]
        [ForeignKey("GitRepositoryId")]
        public GitRepositorySummaryResponse? GitRepository { get; init; }
    }
}
