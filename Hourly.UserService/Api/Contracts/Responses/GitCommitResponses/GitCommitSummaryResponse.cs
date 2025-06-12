using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Contracts.Responses.GitCommitResponses
{
    public class GitCommitSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
    }
}
