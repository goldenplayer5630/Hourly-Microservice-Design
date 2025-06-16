using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class GitRepositoryReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string WebUrl { get; set; } = string.Empty;

        public IReadOnlyCollection<GitCommitReadModel> GitCommits { get; set; } = new List<GitCommitReadModel>();
    }
}
