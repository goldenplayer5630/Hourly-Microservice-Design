using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Contracts.Requests.GitCommitRequests
{
    public class CreateGitCommitRequest
    {
        [Required]
        public Guid RepositoryId { get; init; }

        [Required]
        public string ExtCommitId { get; init; }

        [Required]
        public string ExtCommitShortId { get; init; }

        [Required]
        public string Title { get; init; }

        public string? Comment { get; init; }

        [Required]
        public Guid AuthorId { get; set; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; init; }
    }
}
