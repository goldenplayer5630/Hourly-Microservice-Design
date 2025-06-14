using Hourly.GitService.Abstractions.Queries;
using Hourly.GitService.Infrastructure.Persistence.ReadModels;
using Microsoft.EntityFrameworkCore;

namespace Hourly.GitService.Infrastructure.Queries
{
    public class UserQuery : IUserQuery
    {
        private readonly AppDbContext _context;

        public UserQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserReadModel?> GetById(Guid userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
