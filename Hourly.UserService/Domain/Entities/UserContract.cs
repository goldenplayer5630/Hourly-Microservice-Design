using Hourly.Shared.Enums;
using Hourly.Shared.Exceptions;
using Hourly.UserService.Infrastructure.Persistence.ReadModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.UserService.Domain.Entities
{
    public class UserContract
    {

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public ContractType ContractType { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public double MinWeeklyHours { get; set; }
        [Required]
        public double MaxWeeklyHours { get; set; }

        public double MinimumHoursPerMonth
        {
            get
            {
                return (MinWeeklyHours * 52) / 12;
            }
        }

        public double MaximumHoursPerMonth
        {
            get
            {
                return (MaxWeeklyHours * 52) / 12;
            }
        }

        public double? GrossHourlyRate { get; set; }
        public int? HolidayHoursPercentage { get; set; }
        public bool MonthlyPaidHolidayHours { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ContractFilePath { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        public ICollection<WorkSessionReadModel> WorkSessions { get; init; } = new List<WorkSessionReadModel>();

        public void Update(UserContract updated)
        {
            if (updated == null) throw new ArgumentNullException(nameof(updated));
            Name = updated.Name;
            ContractType = updated.ContractType;
            IsActive = updated.IsActive;
            MinWeeklyHours = updated.MinWeeklyHours;
            MaxWeeklyHours = updated.MaxWeeklyHours;
            GrossHourlyRate = updated.GrossHourlyRate;
            HolidayHoursPercentage = updated.HolidayHoursPercentage;
            MonthlyPaidHolidayHours = updated.MonthlyPaidHolidayHours;
            StartDate = updated.StartDate;
            EndDate = updated.EndDate;
            ContractFilePath = updated.ContractFilePath;
            Description = updated.Description;
            UpdatedAt = DateTime.UtcNow;
            Validate();
        }

        public void AssignToUser(User user)
        {
            UserId = user.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate()
        {
            if (MinWeeklyHours < 0 || MaxWeeklyHours < 0)
                throw new DomainValidationException("Weekly hours cannot be negative.");
            if (MinWeeklyHours > MaxWeeklyHours)
                throw new DomainValidationException("Minimum weekly hours cannot exceed maximum weekly hours.");
            if (StartDate > EndDate)
                throw new DomainValidationException("Start date cannot be after end date.");
            if (!IsActive && EndDate == null)
                throw new DomainValidationException("Inactive contracts must have an end date.");
        }
    }
}
