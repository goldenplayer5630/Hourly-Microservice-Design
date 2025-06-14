using Hourly.GitService.Api.Contracts.Requests.GitRepositoryResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.GitService.Api.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepositorySummaryResponse Repository { get; internal set; }
    }
}
