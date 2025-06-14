using Hourly.UserService.Abstractions.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Abstractions.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {
        [ForeignKey("AuthorId")]
        public UserSummaryResponse Author { get; set; } = new UserSummaryResponse();
    }
}
