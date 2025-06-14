using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Api.Contracts.Requests.GitRepositoryResponses
{
    public class GitRepositorySummaryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string WebUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
