using Microsoft.EntityFrameworkCore;
using MiniNetflix.Identity.Data;
using MiniNetflix.Identity.Data.Models;
using MiniNetflix.Identity.Errors;
using MiniNetflix.Identity.Repositories.Interfaces;
using Npgsql;

namespace MiniNetflix.Identity.Repositories.Impl
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProfileRepository(ApplicationDbContext context) => _context = context;

        public Task<UserProfile?> GetByUserIdAsync(int userId) =>
            _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);

        public async Task<UserProfile> CreateAsync(UserProfile profile)
        {
            _context.UserProfiles.Add(profile);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg
                && pg.SqlState == PostgresErrorCodes.UniqueViolation
                && pg.ConstraintName == "IX_UserProfiles_UserId")
            {
                throw new ConflictException("profile_already_exists", "Профиль пользователя уже существует");
            }
            return profile;
        }

        public async Task<UserProfile> UpdateAsync(UserProfile profile)
        {
            _context.UserProfiles.Update(profile);
            await _context.SaveChangesAsync();
            return profile;
        }

        public async Task<bool> DeleteByUserIdAsync(int userId)
        {
            var profile = await GetByUserIdAsync(userId);
            if (profile == null) return false;
            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
