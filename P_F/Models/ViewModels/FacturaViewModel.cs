using P_F.Models.Entities;

namespace P_F.Models.ViewModels
{
    public class FacturaViewModel
    {
        // Datos de la factura
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        // Cliente
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteDocumento { get; set; } = string.Empty;
        public string ClienteDireccion { get; set; } = string.Empty;
        public string ClienteTelefono { get; set; } = string.Empty;
        public string ClienteEmail { get; set; } = string.Empty;

        // Orden de trabajo relacionada
        public int? OrdenTrabajoId { get; set; }
        public string? NumeroOrden { get; set; }
        public string? VehiculoInfo { get; set; }

        // Detalles
        public List<DetalleFacturaItem> Detalles { get; set; } = new();

        // Totales
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        // Pagos
        public List<PagoItem> Pagos { get; set; } = new();
        public decimal TotalPagado { get; set; }
        public decimal Saldo { get; set; }
        public bool Pagada => Saldo <= 0;

        // Metadatos
        public string? Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
    }

    public class DetalleFacturaItem
    {
        public string Concepto { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

    public class PagoItem
    {
        public int PagoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string MetodoPagoTexto => MetodoPago;
        public string? Referencia { get; set; }
        public string? Observaciones { get; set; }
    }

    public class CrearFacturaViewModel
    {
        public int ClienteId { get; set; }
        public int? OrdenTrabajoId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Descuento { get; set; }
        public string? Observaciones { get; set; }

        // Para selección
        public List<Cliente>? ClientesDisponibles { get; set; }
        public List<OrdenTrabajo>? OrdenesDisponibles { get; set; }
        
        // Detalles manuales (si no es desde orden)
        public List<DetalleFacturaManual> DetallesManual { get; set; } = new();
    }

    public class DetalleFacturaManual
    {
        public string Concepto { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class RegistrarPagoViewModel
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public decimal TotalFactura { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }

        // Nuevo pago
        public decimal MontoPago { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string? Referencia { get; set; }
        public string? Observaciones { get; set; }
    }
}
