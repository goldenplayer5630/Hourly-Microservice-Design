using Hourly.Shared.Enums;
using Hourly.Shared.Exceptions;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Domain.Entities
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public Guid? DepartmentId { get; private set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; private set; }

        public string? GitEmail { get; set; }

        public string? GitUsername { get; set; }

        public string? GitAccessToken { get; set; }

        [Required]
        public float TVTHourBalance { get; set; }

        public ICollection<GitCommitReadModel> GitCommits { get; init; } = new List<GitCommitReadModel>();

        public ICollection<UserContract> Contracts { get; init; } = new List<UserContract>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AssignToDepartment(Department department)
        {
            DepartmentId = department.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveFromDepartment()
        {
            DepartmentId = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(User updated)
        {
            Name = updated.Name;
            Email = updated.Email;
            GitEmail = updated.GitEmail;
            GitUsername = updated.GitUsername;
            GitAccessToken = updated.GitAccessToken;
            TVTHourBalance = updated.TVTHourBalance;
            UpdatedAt = DateTime.UtcNow;
            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainValidationException("Name is required.");
            }
            if (string.IsNullOrWhiteSpace(Email) || !new EmailAddressAttribute().IsValid(Email))
            {
                throw new DomainValidationException("A valid email is required.");
            }
        }
    }
}
