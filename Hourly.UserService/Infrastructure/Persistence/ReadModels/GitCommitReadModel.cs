﻿using Hourly.UserService.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Hourly.UserService.Infrastructure.Persistence.ReadModels
{
    public class GitCommitReadModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public User? Author { get; init; }
    }
}
