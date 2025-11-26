using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P_F.Services;
using P_F.Authorization;
using P_F.Data;

namespace P_F.Controllers;

public class UsuariosController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;
    private readonly ApplicationDbContext _context;

    public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = _userManager.Users.ToList();
        var usuariosConRoles = new List<dynamic>();

        foreach (var usuario in usuarios)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
            usuariosConRoles.Add(new
            {
                Id = usuario.Id,
                Email = usuario.Email,
                UserName = usuario.UserName,
                Roles = string.Join(", ", roles),
                EmailConfirmed = usuario.EmailConfirmed
            });
        }

        // Usar los roles definidos en el código en lugar de consultar la base de datos
        // Esto evita duplicados y asegura consistencia
        var rolesDefinidos = Roles.GetAllRoles()
            .Select(r => new { Name = r })
            .ToList();
        
        ViewBag.Roles = rolesDefinidos;
        return View(usuariosConRoles);
    }

    [HttpPost]
    public async Task<IActionResult> CrearUsuario(string email, string password, string rol)
    {
        try
        {
            var usuario = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var resultado = await _userManager.CreateAsync(usuario, password);
            if (resultado.Succeeded)
            {
                if (!string.IsNullOrEmpty(rol))
                {
                    await _userManager.AddToRoleAsync(usuario, rol);
                }
                TempData["SuccessMessage"] = "Usuario creado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = string.Join(", ", resultado.Errors.Select(e => e.Description));
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al crear usuario: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> AsignarRol(string userId, string rol)
    {
        try
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario != null)
            {
                // Remover roles existentes
                var rolesActuales = await _userManager.GetRolesAsync(usuario);
                await _userManager.RemoveFromRolesAsync(usuario, rolesActuales);

                // Asignar nuevo rol
                await _userManager.AddToRoleAsync(usuario, rol);
                TempData["SuccessMessage"] = "Rol asignado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al asignar rol: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> EliminarUsuario(string userId)
    {
        try
        {
            var usuario = await _userManager.FindByIdAsync(userId);
            if (usuario != null)
            {
                var resultado = await _userManager.DeleteAsync(usuario);
                if (resultado.Succeeded)
                {
                    TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = string.Join(", ", resultado.Errors.Select(e => e.Description));
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al eliminar usuario: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> InicializarSistema()
    {
        try
        {
            TempData["InfoMessage"] = "El sistema ya está inicializado. Usuario administrador: admin@tallerpyf.com, Contraseña: Admin123!";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> CargarDatosPrueba()
    {
        try
        {
            await SeedLargeData.SeedAsync(_context);
            TempData["SuccessMessage"] = "✅ Datos de prueba cargados exitosamente. Se crearon 100 clientes, 150 vehículos, 200 órdenes y más.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al cargar datos de prueba: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> LimpiarDatosPrueba()
    {
        try
        {
            // Eliminar en orden correcto para respetar las relaciones de clave foránea
            
            // 1. Primero eliminar las relaciones muchos a muchos
            var ordenesServicios = await _context.OrdenTrabajoServicios.ToListAsync();
            _context.OrdenTrabajoServicios.RemoveRange(ordenesServicios);
            
            var ordenesRepuestos = await _context.OrdenTrabajoRepuestos.ToListAsync();
            _context.OrdenTrabajoRepuestos.RemoveRange(ordenesRepuestos);

            // 2. Eliminar registros dependientes
            var registrosTiempo = await _context.RegistrosTiempo.ToListAsync();
            _context.RegistrosTiempo.RemoveRange(registrosTiempo);

            var movimientosInventario = await _context.MovimientosInventario.ToListAsync();
            _context.MovimientosInventario.RemoveRange(movimientosInventario);

            var facturas = await _context.Facturas.ToListAsync();
            _context.Facturas.RemoveRange(facturas);

            var ordenesTrabajo = await _context.OrdenesTrabajo.ToListAsync();
            _context.OrdenesTrabajo.RemoveRange(ordenesTrabajo);

            // 3. Eliminar entidades principales
            var vehiculos = await _context.Vehiculos.ToListAsync();
            _context.Vehiculos.RemoveRange(vehiculos);

            var clientes = await _context.Clientes.ToListAsync();
            _context.Clientes.RemoveRange(clientes);

            var repuestos = await _context.Repuestos.ToListAsync();
            _context.Repuestos.RemoveRange(repuestos);

            var empleados = await _context.Empleados.ToListAsync();
            _context.Empleados.RemoveRange(empleados);

            var servicios = await _context.Servicios.ToListAsync();
            _context.Servicios.RemoveRange(servicios);

            // 4. Guardar cambios
            await _context.SaveChangesAsync();

            // 5. Resetear los contadores de identidad (opcional, para SQL Server)
            try
            {
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Clientes', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Vehiculos', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Repuestos', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Empleados', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Servicios', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('OrdenesTrabajo', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Facturas', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('RegistrosTiempo', RESEED, 0)");
                await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('MovimientosInventario', RESEED, 0)");
            }
            catch
            {
                // Si falla el reset de identity (ej: SQLite), continuar sin error
            }

            TempData["SuccessMessage"] = "🗑️ Todos los datos han sido eliminados exitosamente. La base de datos está limpia.";
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error al limpiar datos: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }
}