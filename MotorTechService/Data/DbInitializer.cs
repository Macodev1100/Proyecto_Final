using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorTechService.Authorization;

namespace MotorTechService.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                Console.WriteLine("Verificando conexión a la base de datos...");
                if (!await context.Database.CanConnectAsync())
                {
                    Console.WriteLine("No se puede conectar a la base de datos.");
                    throw new Exception("No se puede conectar a la base de datos.");
                }

                Console.WriteLine("Conexión exitosa. Creando base de datos si es necesario...");
                await context.Database.EnsureCreatedAsync();

                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Aplicando {pendingMigrations.Count()} migraciones pendientes...");
                    await context.Database.MigrateAsync();
                }

                // Crear roles
                string[] roles = { "Administrador", "Mecanico", "Recepcionista", "Supervisor" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        Console.WriteLine($"Creando rol: {role}");
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                // Crear usuario admin
                var adminEmail = "admin@tallerpyf.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new IdentityUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(adminUser, "Admin123!");
                    if (result.Succeeded)
                    {
                        Console.WriteLine("Usuario administrador creado y asignado al rol Administrador.");
                        await userManager.AddToRoleAsync(adminUser, "Administrador");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al inicializar la base de datos: {ex.Message}");
                throw;
            }
        }
    }
}



