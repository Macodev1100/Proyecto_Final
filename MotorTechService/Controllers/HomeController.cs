using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MotorTechService.Models;
using MotorTechService.Data;

namespace MotorTechService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Si está autenticado, redirigir al dashboard
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction(nameof(Dashboard));
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            // Órdenes activas (no completadas ni canceladas)
            var ordenesActivas = await _context.OrdenesTrabajo.CountAsync(o => o.Estado != MotorTechService.Models.Entities.EstadoOrden.Completada && o.Estado != MotorTechService.Models.Entities.EstadoOrden.Cancelada);

            // Ventas del mes actual (sumar total de facturas emitidas este mes)
            var primerDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var ventasMes = await _context.Facturas
                .Where(f => f.FechaEmision >= primerDiaMes && f.FechaEmision <= DateTime.Now)
                .SumAsync(f => (decimal?)f.Total) ?? 0;

            // Total clientes
            var totalClientes = await _context.Clientes.CountAsync();

            // Stock crítico (repuestos con stock actual <= stock mínimo)
            var stockCritico = await _context.Repuestos.CountAsync(r => r.StockActual <= r.StockMinimo);

            // Nuevos clientes esta semana
            var inicioSemana = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var nuevosClientesSemana = await _context.Clientes.CountAsync(c => c.FechaRegistro >= inicioSemana);

            // Variación ventas mes anterior
            var primerDiaMesAnterior = primerDiaMes.AddMonths(-1);
            var ultimoDiaMesAnterior = primerDiaMes.AddDays(-1);
            var ventasMesAnterior = await _context.Facturas
                .Where(f => f.FechaEmision >= primerDiaMesAnterior && f.FechaEmision <= ultimoDiaMesAnterior)
                .SumAsync(f => (decimal?)f.Total) ?? 0;
            var variacionVentasMes = ventasMesAnterior > 0 ? (int)Math.Round(((ventasMes - ventasMesAnterior) / ventasMesAnterior) * 100) : 0;

            var model = new {
                OrdenesActivas = ordenesActivas,
                VentasMes = ventasMes,
                TotalClientes = totalClientes,
                StockCritico = stockCritico,
                NuevosClientesSemana = nuevosClientesSemana,
                VariacionVentasMes = variacionVentasMes
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
