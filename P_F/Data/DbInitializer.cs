using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P_F.Authorization;

namespace P_F.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Asegurar que la base de datos está creada
            await context.Database.MigrateAsync();

            // Crear roles si no existen
            await CreateRoles(roleManager);

            // Crear usuarios de prueba si no existen
            await CreateDefaultUsers(userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = Roles.GetAllRoles();

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task CreateDefaultUsers(UserManager<IdentityUser> userManager)
        {
            // Usuario Administrador
            if (await userManager.FindByEmailAsync("admin@tallerpyf.com") == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@tallerpyf.com",
                    Email = "admin@tallerpyf.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Administrador);
                }
            }

            // Usuario Supervisor
            if (await userManager.FindByEmailAsync("supervisor@tallerpyf.com") == null)
            {
                var supervisorUser = new IdentityUser
                {
                    UserName = "supervisor@tallerpyf.com",
                    Email = "supervisor@tallerpyf.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(supervisorUser, "Supervisor123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(supervisorUser, Roles.Supervisor);
                }
            }

            // Usuario Mecánico
            if (await userManager.FindByEmailAsync("mecanico@tallerpyf.com") == null)
            {
                var mecanicoUser = new IdentityUser
                {
                    UserName = "mecanico@tallerpyf.com",
                    Email = "mecanico@tallerpyf.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(mecanicoUser, "Mecanico123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(mecanicoUser, Roles.Mecanico);
                }
            }

            // Usuario Recepcionista
            if (await userManager.FindByEmailAsync("recepcionista@tallerpyf.com") == null)
            {
                var recepcionistaUser = new IdentityUser
                {
                    UserName = "recepcionista@tallerpyf.com",
                    Email = "recepcionista@tallerpyf.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(recepcionistaUser, "Recepcionista123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(recepcionistaUser, Roles.Recepcionista);
                }
            }
        }
    }
}
