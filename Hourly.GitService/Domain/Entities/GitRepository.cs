using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Domain.Entities
{
    public class GitRepository
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

        public ICollection<GitCommit> GitCommits { get; set; } = new List<GitCommit>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
