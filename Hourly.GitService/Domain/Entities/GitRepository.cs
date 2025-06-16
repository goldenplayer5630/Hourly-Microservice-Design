using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Domain.Entities
{
    public class GitRepository
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

        public ICollection<GitCommit> GitCommits { get; init; } = new List<GitCommit>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
