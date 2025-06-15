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
        public string ExtCommitId { get; set; }

        [Required]
        public string ExtCommitShortId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Comment { get; set; }

        [Required]
        public Guid? AuthorId { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public UserReadModel? Author { get; set; } = null!;

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
