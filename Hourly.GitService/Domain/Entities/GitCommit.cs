using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.GitService.Domain.Entities
{
    public class GitCommit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid GitRepositoryId { get; set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepository GitRepository { get; set; } = null!;

        [Required]
        public string ExtCommitId { get; set; } = string.Empty;

        [Required]
        public string ExtCommitShortId { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Comment { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public UserReadModel? Author { get; init; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
