using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Domain.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; } // Store as JSON string

        public ICollection<User> Users { get; init; } = new List<User>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
