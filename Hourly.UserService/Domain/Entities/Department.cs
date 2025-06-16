using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; init; } = new List<User>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
