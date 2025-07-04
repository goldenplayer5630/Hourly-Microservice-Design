﻿using Hourly.TimeTrackingService.Infrastructure.Persistence;
using Hourly.TimeTrackingService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Hourly.TimeTrackingService.Infrastructure.Queries
{
    public class UserContractQuery : IUserContractQuery
    {
        private readonly AppDbContext _context;

        public UserContractQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserContractReadModel?> GetById(Guid contractId)
        {
            return await _context.UserContracts
                .Include(uc => uc.User)
                .FirstOrDefaultAsync(uc => uc.Id == contractId);
        }

        public async Task<bool> Exists(Guid contractId)
        {
            return await _context.UserContracts.AnyAsync(uc => uc.Id == contractId);
        }
    }
}
