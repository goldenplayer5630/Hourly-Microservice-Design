using System.ComponentModel.DataAnnotations;

namespace Hourly.GitService.Infrastructure.Persistence.ReadModels
{
    public class UserReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }
    }
}
