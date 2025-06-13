using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetById(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Include(u => u.GitCommits)
                .Include(u => u.Contracts)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? user : null) ?? throw new InvalidOperationException();
        }

        public async Task<User> Update(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? user : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
