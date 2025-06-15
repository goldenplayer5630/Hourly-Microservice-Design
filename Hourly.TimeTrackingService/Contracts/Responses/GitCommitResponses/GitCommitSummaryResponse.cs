using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.GitCommitResponses
{
    public class GitCommitSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; internal set; }

        [Required]
        public string ExtCommitId { get; set; }

        [Required]
        public string ExtCommitShortId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; set; }

    }
}
