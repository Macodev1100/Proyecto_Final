using MotorTechService.Models.Entities;

namespace MotorTechService.Models.ViewModels
{
    public class OrdenTrabajoViewModel
    {
        // Datos de la orden
        public int OrdenTrabajoId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public EstadoOrden Estado { get; set; }
        public string? Observaciones { get; set; }

        // Cliente y vehículo
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteTelefono { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;

        public int VehiculoId { get; set; }
        public string VehiculoPlaca { get; set; } = string.Empty;
        public string VehiculoMarca { get; set; } = string.Empty;
        public string VehiculoModelo { get; set; } = string.Empty;
        public int VehiculoAnio { get; set; }

        // Empleado responsable
        public int? EmpleadoId { get; set; }
        public string? EmpleadoNombre { get; set; }

        // Servicios y repuestos
        public List<ServicioOrdenItem> Servicios { get; set; } = new();
        public List<RepuestoOrdenItem> Repuestos { get; set; } = new();

        // Totales
        public decimal SubtotalServicios { get; set; }
        public decimal SubtotalRepuestos { get; set; }
        public decimal Total { get; set; }

        // Historial de estados
        public List<HistorialEstadoItem> HistorialEstados { get; set; } = new();

        // Datos para edición
        public List<Empleado>? EmpleadosDisponibles { get; set; }
        public List<Servicio>? ServiciosDisponibles { get; set; }
        public List<Repuesto>? RepuestosDisponibles { get; set; }
    }

    public class ServicioOrdenItem
    {
        public int ServicioId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad;
    }

    public class RepuestoOrdenItem
    {
        public int RepuestoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Codigo { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public int StockDisponible { get; set; }
        public decimal Subtotal => PrecioVenta * Cantidad;
    }

    public class HistorialEstadoItem
    {
        public DateTime Fecha { get; set; }
        public string EstadoAnterior { get; set; } = string.Empty;
        public string EstadoNuevo { get; set; } = string.Empty;
        public string EmpleadoNombre { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
    }
}
