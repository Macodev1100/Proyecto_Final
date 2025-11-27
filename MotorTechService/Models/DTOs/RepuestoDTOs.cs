using System.ComponentModel.DataAnnotations;

namespace MotorTechService.Models.DTOs
{
    /// <summary>
    /// DTO para creación de repuesto
    /// </summary>
    public class RepuestoCreateDTO
    {
        [Required(ErrorMessage = "El código es requerido")]
        [StringLength(50)]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        public int CategoriaRepuestoId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioCompra { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecioVenta { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockActual { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int StockMinimo { get; set; }

        [StringLength(100)]
        public string? Ubicacion { get; set; }

        [StringLength(100)]
        public string? Proveedor { get; set; }
    }

    /// <summary>
    /// DTO para actualización de repuesto
    /// </summary>
    public class RepuestoUpdateDTO : RepuestoCreateDTO
    {
        [Required]
        public int RepuestoId { get; set; }
    }

    /// <summary>
    /// DTO para lectura de repuesto
    /// </summary>
    public class RepuestoDTO
    {
        public int RepuestoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int CategoriaRepuestoId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal MargenGanancia => PrecioVenta - PrecioCompra;
        public decimal PorcentajeGanancia => PrecioCompra > 0 ? ((PrecioVenta - PrecioCompra) / PrecioCompra * 100) : 0;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool StockBajo => StockActual <= StockMinimo;
        public string? Ubicacion { get; set; }
        public string? Proveedor { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas
    /// </summary>
    public class RepuestoListDTO
    {
        public int RepuestoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public int StockActual { get; set; }
        public bool StockBajo { get; set; }
    }

    /// <summary>
    /// DTO para movimiento de inventario
    /// </summary>
    public class MovimientoInventarioDTO
    {
        [Required]
        public int RepuestoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required]
        public TipoMovimiento TipoMovimiento { get; set; }

        [Required]
        [StringLength(200)]
        public string Motivo { get; set; } = string.Empty;

        [Required]
        public int EmpleadoId { get; set; }
    }

    public enum TipoMovimiento
    {
        Entrada,
        Salida
    }
}
