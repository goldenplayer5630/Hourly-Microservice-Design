using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class GitRepositoryReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public IReadOnlyCollection<GitCommitReadModel> GitCommits { get; set; } = new List<GitCommitReadModel>();
    }
}
