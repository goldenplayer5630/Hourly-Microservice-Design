using Hourly.Shared.Enums;
using Hourly.TimeTrackingService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
