namespace MotorTechService.Models.ViewModels
{
    public class DashboardViewModel
    {
        // Estadísticas generales
        public int TotalClientes { get; set; }
        public int TotalVehiculos { get; set; }
        public int OrdenesActivas { get; set; }
        public int OrdenesPendientes { get; set; }
        public decimal IngresosDelMes { get; set; }
        public decimal IngresosDelAnio { get; set; }

        // Gráficos
        public List<VentasPorMesData> VentasPorMes { get; set; } = new();
        public List<ServicioMasVendidoData> ServiciosMasVendidos { get; set; } = new();
        public List<EstadoOrdenesData> EstadosOrdenes { get; set; } = new();

        // Alertas y notificaciones
        public int RepuestosBajoStock { get; set; }
        public List<RepuestoBajoStockItem> RepuestosBajoStockDetalle { get; set; } = new();
        public List<OrdenVencidaItem> OrdenesVencidas { get; set; } = new();
        public List<OrdenProximaVencerItem> OrdenesProximasVencer { get; set; } = new();

        // Actividad reciente
        public List<OrdenRecienteItem> OrdenesRecientes { get; set; } = new();
        public List<FacturaRecienteItem> FacturasRecientes { get; set; } = new();
    }

    public class VentasPorMesData
    {
        public string Mes { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public int CantidadOrdenes { get; set; }
    }

    public class ServicioMasVendidoData
    {
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }

    public class EstadoOrdenesData
    {
        public string Estado { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public string Color { get; set; } = string.Empty;
    }

    public class RepuestoBajoStockItem
    {
        public int RepuestoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public decimal Precio { get; set; }
    }

    public class OrdenVencidaItem
    {
        public int OrdenId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaEstimada { get; set; }
        public int DiasVencidos { get; set; }
    }

    public class OrdenProximaVencerItem
    {
        public int OrdenId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime FechaEstimada { get; set; }
        public int DiasRestantes { get; set; }
    }

    public class OrdenRecienteItem
    {
        public int OrdenId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public decimal Total { get; set; }
    }

    public class FacturaRecienteItem
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public decimal Pendiente { get; set; }
    }
}
