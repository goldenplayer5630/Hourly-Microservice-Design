using Hourly.Shared.Exceptions;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.TimeTrackingService.Domain.Entities
{
    public class WorkSession
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserContractId { get; init; }
        [ForeignKey("UserContractId")]
        public UserContractReadModel? UserContract { get; set; }
        public string TaskDescription { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float BreakTime { get; set; }
        public float Factor { get; set; }
        public float TVTAccruedHours { get; set; } = 0;
        public float TVTUsedHours { get; set; } = 0;
        public bool WBSO { get; set; }
        public bool Locked { get; set; }
        public string? OtherRemarks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<GitCommitReadModel> GitCommits { get; set; } = new List<GitCommitReadModel>();

        public float RawEffectiveHours
        {
            get
            {
                var total = (float)((EndTime - StartTime).TotalHours) - BreakTime;
                if (total < 0)
                    throw new DomainValidationException("Raw effective hours cannot be negative.");
                return total;
            }
        }

        public float NetEffectiveHours
        {
            get
            {
                if (TVTUsedHours > 0 && TVTAccruedHours > 0)
                    throw new DomainValidationException("Cannot both accrue and use TVT hours in the same work session.");

                var net = (TVTAccruedHours > 0 ? RawEffectiveHours - TVTAccruedHours : RawEffectiveHours + TVTUsedHours) * Factor;

                if (net < 0)
                    throw new DomainValidationException("Net effective hours cannot be negative.");
                return net;
            }
        }

        public void AddGitCommit(GitCommitReadModel gitCommit)
        {
            if (GitCommits.Any(gc => gc.Id == gitCommit.Id))
                throw new DomainValidationException("Git commit is already associated with this work session.");
            GitCommits.Add(gitCommit);
        }

        public void RemoveGitCommit(GitCommitReadModel gitCommit)
        {
            if (!GitCommits.Any(gc => gc.Id == gitCommit.Id))
                throw new DomainValidationException("Git commit is not associated with this work session.");
            GitCommits.Remove(gitCommit);
        }

        public void AssignToUserContract(UserContractReadModel userContract)
        {
            UserContract = userContract;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate()
        {
            if (StartTime > EndTime)
                throw new DomainValidationException("Start time must be before or equal to end time.");

            if (Factor < 0)
                throw new DomainValidationException("Factor cannot be negative.");

            if (TVTAccruedHours > 0 && TVTUsedHours > 0)
                throw new DomainValidationException("Cannot both accrue and use TVT hours in the same work session.");

            if (TVTAccruedHours < 0 || TVTUsedHours < 0)
                throw new DomainValidationException("TVTAccruedHours and TVTUsedHours cannot be negative.");

            if (RawEffectiveHours < 0 || NetEffectiveHours < 0)
                throw new DomainValidationException("Total effective and net effective hours cannot be negative.");

            if (BreakTime < 0)
                throw new DomainValidationException("Break time cannot be negative.");

            if (BreakTime >= RawEffectiveHours)
                throw new DomainValidationException("Break time cannot exceed net effective hours.");

            if (!IsValid15MinuteInterval(StartTime.Minute) || !IsValid15MinuteInterval(EndTime.Minute))
                throw new DomainValidationException("Start and end time must be in 15-minute intervals.");

            if (Math.Abs(TVTAccruedHours % 0.25) > 0.0001)
                throw new DomainValidationException("TVTAccruedHours and TVTUsedHours must be in increments of 15 minutes.");

            var today = DateTime.UtcNow.Date;
            if (StartTime.Date > today || EndTime.Date > today)
                throw new DomainValidationException("Work session start and end times cannot be in the future.");

            if (Locked)
                throw new DomainValidationException("Cannot modify a locked work session.");
        }

        public void Update(WorkSession updated)
        {
            TaskDescription = updated.TaskDescription;
            StartTime = updated.StartTime;
            EndTime = updated.EndTime;
            BreakTime = updated.BreakTime;
            Factor = updated.Factor;
            TVTAccruedHours = updated.TVTAccruedHours;
            TVTUsedHours = updated.TVTUsedHours;
            WBSO = updated.WBSO;
            Locked = updated.Locked;
            OtherRemarks = updated.OtherRemarks;
            CreatedAt = updated.CreatedAt;
            UpdatedAt = DateTime.UtcNow;

            Validate();
        }

        private bool IsValid15MinuteInterval(int minute)
        {
            return minute == 0 || minute == 15 || minute == 30 || minute == 45;
        }
    }
}
