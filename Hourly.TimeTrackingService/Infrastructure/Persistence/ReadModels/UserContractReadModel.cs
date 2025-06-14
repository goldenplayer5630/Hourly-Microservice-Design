using Hourly.TimeTrackingService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels
{
    public class UserContractReadModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("UserId")]
        public UserReadModel User { get; set; } = null!;
        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();
    }
}
