using Microsoft.EntityFrameworkCore;
using MiniNetflix.Identity.Constants;
using MiniNetflix.Identity.Data.Models;

namespace MiniNetflix.Identity.Data
{
    public static class DbInitializer
    {
        public static void MigrateAndSeed(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            db.Database.Migrate();

            SeedRoles(db, AppRoles.DefaultRoles);

            db.SaveChanges();
        }

        private static void SeedRoles(ApplicationDbContext db, params string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (!db.Roles.Any(r => r.Name == roleName))
                {
                    db.Roles.Add(new Role { Name = roleName });
                }
            }
        }
    }
}
