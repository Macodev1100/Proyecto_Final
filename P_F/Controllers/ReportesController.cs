using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P_F.Data;
using P_F.Models;
using P_F.Models.Entities;
using P_F.Services;

namespace P_F.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPdfService _pdfService;

        public ReportesController(ApplicationDbContext context, IPdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VentasPorPeriodo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VentasPorPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = _context.Facturas
                .Where(f => f.FechaEmision >= fechaInicio && f.FechaEmision <= fechaFin && f.Estado == EstadoFactura.Pagada)
                .GroupBy(f => f.FechaEmision.Date)
                .Select(g => new VentaDiaria
                {
                    Fecha = g.Key,
                    Total = g.Sum(f => f.Total)
                })
                .OrderBy(v => v.Fecha)
                .ToList();

            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.TotalPeriodo = ventas.Sum(v => v.Total);
            
            return View("VentasResultado", ventas);
        }

        [HttpGet]
        public IActionResult InventarioCritico()
        {
            var repuestosCriticos = _context.Repuestos
                .Where(r => r.StockActual <= r.StockMinimo)
                .OrderBy(r => r.StockActual)
                .ToList();

            return View(repuestosCriticos);
        }

        [HttpGet]
        public IActionResult ProductividadEmpleados(DateTime? fechaInicio, DateTime? fechaFin)
        {
            fechaInicio ??= DateTime.Now.AddDays(-30);
            fechaFin ??= DateTime.Now;

            var empleados = _context.Empleados.Where(e => e.Activo).ToList();
            var empleadosProductividad = new List<P_F.ViewModels.EmpleadoProductividad>();

            foreach (var empleado in empleados)
            {
                var ordenesCompletadas = _context.OrdenesTrabajo
                    .Count(o => o.EmpleadoAsignadoId == empleado.EmpleadoId && 
                              o.Estado == EstadoOrden.Completada &&
                              o.FechaIngreso >= fechaInicio && 
                              o.FechaIngreso <= fechaFin);

                var ordenesEnProceso = _context.OrdenesTrabajo
                    .Count(o => o.EmpleadoAsignadoId == empleado.EmpleadoId && 
                              o.Estado == EstadoOrden.EnProceso);

                var totalIngresos = _context.OrdenesTrabajo
                    .Where(o => o.EmpleadoAsignadoId == empleado.EmpleadoId && 
                              o.Estado == EstadoOrden.Completada &&
                              o.FechaIngreso >= fechaInicio && 
                              o.FechaIngreso <= fechaFin)
                    .Sum(o => (decimal?)o.Total) ?? 0;

                var ordenesRecientes = _context.OrdenesTrabajo
                    .Where(o => o.EmpleadoAsignadoId == empleado.EmpleadoId)
                    .OrderByDescending(o => o.FechaIngreso)
                    .Take(5)
                    .ToList();

                // Calcular eficiencia básica
                var eficiencia = ordenesCompletadas > 0 ? 
                    Math.Min(100, (ordenesCompletadas * 20) + (totalIngresos > 1000 ? 20 : 0)) : 0;

                empleadosProductividad.Add(new P_F.ViewModels.EmpleadoProductividad
                {
                    EmpleadoId = empleado.EmpleadoId,
                    NombreCompleto = $"{empleado.Nombre} {empleado.Apellido}",
                    Especialidad = empleado.Especialidad ?? "General",
                    OrdenesCompletadas = ordenesCompletadas,
                    OrdenesEnProceso = ordenesEnProceso,
                    TotalIngresos = totalIngresos,
                    PromedioTiempoOrden = ordenesCompletadas > 0 ? 2.5m : 0,
                    EficienciaCalculada = eficiencia,
                    OrdenesRecientes = ordenesRecientes
                });
            }

            var viewModel = new P_F.ViewModels.ReporteProductividadViewModel
            {
                EmpleadosProductividad = empleadosProductividad,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                TotalIngresosPeriodo = empleadosProductividad.Sum(e => e.TotalIngresos),
                TotalOrdenesPeriodo = empleadosProductividad.Sum(e => e.OrdenesCompletadas)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> OrdenesPorEstado(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Establecer fechas por defecto
            if (!fechaInicio.HasValue)
                fechaInicio = DateTime.Now.AddMonths(-3);
            if (!fechaFin.HasValue)
                fechaFin = DateTime.Now;

            var ordenesPorEstado = await _context.OrdenesTrabajo
                .Where(o => o.FechaIngreso >= fechaInicio && o.FechaIngreso <= fechaFin)
                .GroupBy(o => o.Estado)
                .Select(g => new
                {
                    Estado = g.Key,
                    Cantidad = g.Count(),
                    TotalMonto = g.Sum(o => o.Total),
                    PromedioMonto = g.Average(o => o.Total)
                })
                .OrderByDescending(x => x.Cantidad)
                .ToListAsync();

            var ordenes = await _context.OrdenesTrabajo
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .Include(o => o.EmpleadoAsignado)
                .Where(o => o.FechaIngreso >= fechaInicio && o.FechaIngreso <= fechaFin)
                .OrderByDescending(o => o.FechaIngreso)
                .Take(100)
                .ToListAsync();

            ViewBag.OrdenesPorEstado = ordenesPorEstado;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.TotalOrdenes = ordenes.Count;
            ViewBag.MontoTotal = ordenes.Sum(o => o.Total);

            return View(ordenes);
        }

        [HttpGet]
        public async Task<IActionResult> ClientesFrecuentes(int? mes, int? año)
        {
            // Establecer período por defecto
            if (!mes.HasValue) mes = DateTime.Now.Month;
            if (!año.HasValue) año = DateTime.Now.Year;

            var fechaInicio = new DateTime(año.Value, mes.Value, 1);
            var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

            var clientesFrecuentes = await _context.Clientes
                .Where(c => c.Activo)
                .Select(c => new
                {
                    Cliente = c,
                    TotalOrdenes = c.OrdenesTrabajo
                        .Where(o => o.FechaIngreso >= fechaInicio && o.FechaIngreso <= fechaFin)
                        .Count(),
                    TotalFacturado = c.OrdenesTrabajo
                        .Where(o => o.FechaIngreso >= fechaInicio && o.FechaIngreso <= fechaFin && o.Estado == EstadoOrden.Completada)
                        .Sum(o => (decimal?)o.Total) ?? 0,
                    UltimaVisita = c.OrdenesTrabajo
                        .OrderByDescending(o => o.FechaIngreso)
                        .Select(o => o.FechaIngreso)
                        .FirstOrDefault()
                })
                .Where(x => x.TotalOrdenes > 0)
                .OrderByDescending(x => x.TotalOrdenes)
                .ThenByDescending(x => x.TotalFacturado)
                .Take(20)
                .ToListAsync();

            ViewBag.Mes = mes;
            ViewBag.Año = año;
            ViewBag.FechaInicio = fechaInicio;
            ViewBag.FechaFin = fechaFin;
            ViewBag.TotalOrdenes = clientesFrecuentes.Sum(c => c.TotalOrdenes);
            ViewBag.TotalFacturado = clientesFrecuentes.Sum(c => c.TotalFacturado);
            ViewBag.PromedioCliente = clientesFrecuentes.Any() ? clientesFrecuentes.Average(c => c.TotalFacturado) : 0;

            return View(clientesFrecuentes);
        }

        // Métodos para generar PDFs
        [HttpPost]
        public async Task<IActionResult> VentasPdf(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var pdfBytes = await _pdfService.GenerarReporteVentasPdfAsync(fechaInicio, fechaFin);
                return File(pdfBytes, "application/pdf", $"ReporteVentas_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToAction("VentasPorPeriodo");
            }
        }

        [HttpGet]
        public async Task<IActionResult> InventarioPdf()
        {
            try
            {
                var pdfBytes = await _pdfService.GenerarReporteInventarioPdfAsync();
                return File(pdfBytes, "application/pdf", $"ReporteInventario_{DateTime.Now:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToAction("InventarioCritico");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EmpleadosPdf(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var pdfBytes = await _pdfService.GenerarReporteEmpleadosPdfAsync(fechaInicio, fechaFin);
                return File(pdfBytes, "application/pdf", $"ReporteEmpleados_{fechaInicio:yyyyMMdd}_{fechaFin:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToAction("ProductividadEmpleados");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ClientesFrecuentesPdf()
        {
            try
            {
                var pdfBytes = await _pdfService.GenerarReporteClientesFrecuentesPdfAsync();
                return File(pdfBytes, "application/pdf", $"ReporteClientesFrecuentes_{DateTime.Now:yyyyMMdd}.pdf");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al generar PDF: {ex.Message}";
                return RedirectToAction("ClientesFrecuentes");
            }
        }
    }
}