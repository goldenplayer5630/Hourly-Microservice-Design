using Hourly.TimeTrackingService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class GitCommitReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        [ForeignKey("AuthorId")]
        public UserReadModel Author { get; set; } = null!;
        [Required]
        public DateTime AuthoredDate { get; set; }
        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();
        [Required]
        public Guid GitRepositoryId { get; set; }
        [Required]
        [ForeignKey("GitRepositoryId")]
        public GitRepositoryReadModel GitRepository { get; set; } = null!;
        [Required]
        public string ExtCommitId { get; set; }
        [Required]
        public string ExtCommitShortId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string WebUrl { get; set; }
    }
}
