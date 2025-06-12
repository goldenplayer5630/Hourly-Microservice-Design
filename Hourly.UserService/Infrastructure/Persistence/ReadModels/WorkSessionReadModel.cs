using Hourly.UserService.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Infrastructure.Persistence.ReadModels
{
    public class WorkSessionReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserContractId { get; init; }
        [ForeignKey("UserContractId")]
        public UserContract UserContract { get; private set; } = null!;
    }
}
