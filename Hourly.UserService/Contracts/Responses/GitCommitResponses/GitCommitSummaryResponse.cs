using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Contracts.Responses.GitCommitResponses
{
    public class GitCommitSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid? AuthorId { get; init; }
    }
}
