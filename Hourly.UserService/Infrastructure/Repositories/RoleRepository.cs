using Hourly.Shared.Exceptions;
using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Domain.Entities;
using Hourly.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetById(Guid roleId)
        {
            return await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? role : null) ?? throw new InvalidOperationException();
        }

        public async Task<Role> Update(Role role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                throw new EntityNotFoundException("Role not found!");
            }

            _context.Entry(existingRole).CurrentValues.SetValues(role);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? role : null) ?? throw new InvalidOperationException();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
