using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Contracts.Requests.GitCommitRequests
{
    public class CreateGitCommitRequest
    {
        [Required]
        public Guid RepositoryId { get; init; }

        [Required]
        public string ExtCommitId { get; init; } = string.Empty;

        [Required]
        public string ExtCommitShortId { get; init; } = string.Empty;

        [Required]
        public string Title { get; init; } = string.Empty;

        public string? Comment { get; init; }

        [Required]
        public Guid AuthorId { get; init; }

        public DateTime AuthoredDate { get; init; }

        [Required]
        public string WebUrl { get; init; } = string.Empty;
    }
}
