using Hourly.UserService.Abstractions.Repositories;
using Hourly.UserService.Domain.Entities;
using Hourly.Shared.Exceptions;
using Hourly.UserService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Department?> GetById(Guid departmentId)
        {
            return await _context.Departments
                .Include(d => d.Users)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments
            .Include(d => d.Users)
            .ThenInclude(u => u.Role)
            .ToListAsync();
        }

        public async Task<Department> Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task<Department> Update(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            _context.Entry(existingDepartment).CurrentValues.SetValues(department);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid departmentId)
        {
            var existingDepartment = await _context.Departments.FindAsync(departmentId);

            if (existingDepartment == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            _context.Departments.Remove(existingDepartment);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
