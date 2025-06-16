using System.ComponentModel.DataAnnotations;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class UserReadModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public float TVTHourBalance { get; set; }

        public ICollection<GitCommitReadModel> GitCommits { get; set; } = new List<GitCommitReadModel>();

        public ICollection<UserContractReadModel> Contracts { get; init; } = new List<UserContractReadModel>();
    }
}
