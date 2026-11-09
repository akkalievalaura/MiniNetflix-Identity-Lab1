using Microsoft.EntityFrameworkCore;
using MiniNetflix.Identity.Data;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.Repositories.Interfaces;

namespace MiniNetflix.Identity.Repositories.Impl
{
    /// <inheritdoc />
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public Task<List<Role>> GetAllAsync()
        {
            return _context.Roles
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        /// <inheritdoc />
        public Task<Role?> GetByIdAsync(int id)
        {
            return _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <inheritdoc />
        public Task<Role?> GetByNameAsync(string name)
        {
            return _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        /// <inheritdoc />
        public async Task<Role> CreateAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        /// <inheritdoc />
        public async Task<Role> UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
                return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
