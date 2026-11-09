using Microsoft.EntityFrameworkCore;
using MiniNetflix.Identity.Data;
using MiniNetflix.Identity.Errors;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.Repositories.Interfaces;

namespace MiniNetflix.Identity.Repositories.Impl
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <inheritdoc />
        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <inheritdoc />
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstAsync(u => u.Id == id);
        }

        /// <inheritdoc />
        public async Task<User?> GetUserByUserIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        /// <inheritdoc />
        public async Task UpdatePasswordHashAsync(int id, string passwordHash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("user_not_found", "Пользователь не найден");
            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
        }
    }
}
