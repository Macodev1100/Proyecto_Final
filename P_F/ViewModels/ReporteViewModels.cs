using P_F.Models.Entities;

namespace P_F.ViewModels
{
    public class ReporteFacturasViewModel
    {
        public List<Factura> Facturas { get; set; } = new();
        public decimal TotalFacturado { get; set; }
        public int TotalFacturasPagadas { get; set; }
        public int TotalFacturasPendientes { get; set; }
        public int TotalFacturasVencidas { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? EstadoFiltro { get; set; }
    }

    public class ReporteProductividadViewModel
    {
        public List<EmpleadoProductividad> EmpleadosProductividad { get; set; } = new();
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal TotalIngresosPeriodo { get; set; }
        public int TotalOrdenesPeriodo { get; set; }
    }

    public class EmpleadoProductividad
    {
        public int EmpleadoId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public int OrdenesCompletadas { get; set; }
        public int OrdenesEnProceso { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal PromedioTiempoOrden { get; set; }
        public decimal EficienciaCalculada { get; set; }
        public List<OrdenTrabajo> OrdenesRecientes { get; set; } = new();
    }
}