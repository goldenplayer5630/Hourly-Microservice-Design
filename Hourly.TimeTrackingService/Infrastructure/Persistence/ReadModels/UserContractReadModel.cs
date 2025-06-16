using Hourly.TimeTrackingService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class UserContractReadModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public UserReadModel? User { get; set; }
        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();
    }
}
