using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P_F.Data;
using P_F.Models.Entities;

namespace P_F.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                // Verificar conexión a la base de datos
                Console.WriteLine("Verificando conexión a la base de datos...");
                var canConnect = await context.Database.CanConnectAsync();
                
                if (!canConnect)
                {
                    Console.WriteLine("No se puede conectar a la base de datos. Verificando configuración...");
                    throw new Exception("No se puede establecer conexión con la base de datos SQL Server.");
                }

                Console.WriteLine("Conexión exitosa. Creando base de datos si es necesario...");
                
                // Crear la base de datos si no existe
                await context.Database.EnsureCreatedAsync();
                
                // Aplicar migraciones pendientes si existen
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Aplicando {pendingMigrations.Count()} migraciones pendientes...");
