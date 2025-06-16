using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositorySummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Namespace { get; set; } = string.Empty;

        [Required]
        public string WebUrl { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
