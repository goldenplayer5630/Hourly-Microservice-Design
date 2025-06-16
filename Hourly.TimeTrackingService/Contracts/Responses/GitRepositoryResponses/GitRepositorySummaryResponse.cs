using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositorySummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string ExtRepositoryId { get; init; } = string.Empty;

        [Required]
        public string Name { get; init; } = string.Empty;

        [Required]
        public string Namespace { get; init; } = string.Empty;

        [Required]
        public string WebUrl { get; init; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
