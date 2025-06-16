using Hourly.UserService.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Infrastructure.Persistence.ReadModels
{
    public class WorkSessionReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserContractId { get; set; }
        [ForeignKey("UserContractId")]
        public UserContract? UserContract { get; init; }
    }
}
