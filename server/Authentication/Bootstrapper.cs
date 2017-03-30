using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using RadzenEndToEndAngularApplication.Data;

namespace RadzenEndToEndAngularApplication.Authentication
{
    public static class Bootstrapper
    {
        public static void EnsureIdentitySchema(TestContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }

        public static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = new [] { "Administrator" };

            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role);

                if (!exists)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            foreach (var role in roleManager.Roles.ToList())
            {
                if (!roles.Contains(role.Name))
                {
                    await roleManager.DeleteAsync(role);
                }
            }
        }
    }
}
