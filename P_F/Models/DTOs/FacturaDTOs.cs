using P_F.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace P_F.Models.DTOs
{
    /// <summary>
    /// DTO para creación de factura
    /// </summary>
    public class FacturaCreateDTO
    {
        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClienteId { get; set; }

        public int? OrdenTrabajoId { get; set; }

        [Required(ErrorMessage = "El subtotal es requerido")]
        [Range(0, double.MaxValue)]
        public decimal Subtotal { get; set; }

        [Range(0, 100)]
        public decimal Impuesto { get; set; }

        [Range(0, 100)]
        public decimal Descuento { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Total { get; set; }

        // public EstadoFactura Estado { get; set; } = EstadoFactura.Pendiente;

        public string? Observaciones { get; set; }

        public string? FormaPago { get; set; }
    }

    /// <summary>
    /// DTO para actualización de factura
    /// </summary>
    public class FacturaUpdateDTO : FacturaCreateDTO
    {
        [Required]
        public int FacturaId { get; set; }

        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }

    /// <summary>
    /// DTO para lectura de factura
    /// </summary>
    public class FacturaDTO
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        
        // Cliente
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteDocumento { get; set; } = string.Empty;
        
        // Orden de trabajo (opcional)
        public int? OrdenTrabajoId { get; set; }
        public string? OrdenNumero { get; set; }
        
        // Montos
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Descuento { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente => Total - TotalPagado;
        
        // Estado
        public EstadoFactura Estado { get; set; }
        public string EstadoDescripcion => Estado.ToString();
        
        public string? Observaciones { get; set; }
        public string? FormaPago { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas
    /// </summary>
    public class FacturaListDTO
    {
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaEmision { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public decimal SaldoPendiente { get; set; }
        public EstadoFactura Estado { get; set; }
    }

    /// <summary>
    /// DTO para registro de pago
    /// </summary>
    public class PagoCreateDTO
    {
        [Required]
        public int FacturaId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaPago { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string FormaPago { get; set; } = string.Empty;

        [StringLength(100)]
        public string? NumeroTransaccion { get; set; }

        [StringLength(200)]
        public string? Observaciones { get; set; }
    }

    /// <summary>
    /// DTO para lectura de pago
    /// </summary>
    public class PagoDTO
    {
        public int PagoId { get; set; }
        public int FacturaId { get; set; }
        public string FacturaNumero { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string FormaPago { get; set; } = string.Empty;
        public string? NumeroTransaccion { get; set; }
        public string? Observaciones { get; set; }
    }
}
