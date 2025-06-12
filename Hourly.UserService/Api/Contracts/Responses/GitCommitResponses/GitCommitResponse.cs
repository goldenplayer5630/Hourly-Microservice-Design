using Hourly.Domain.Contracts.Responses.WorkSessionResponses;
using Hourly.UserService.Api.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse : GitCommitSummaryResponse
    {
        [ForeignKey("AuthorId")]
        public UserSummaryResponse Author { get; set; } = new UserSummaryResponse();
    }
}
