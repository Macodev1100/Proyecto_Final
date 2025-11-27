using MotorTechService.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MotorTechService.Models.DTOs
{
    /// <summary>
    /// DTO para creación de orden de trabajo
    /// </summary>
    public class OrdenTrabajoCreateDTO
    {
        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El vehículo es requerido")]
        public int VehiculoId { get; set; }

        [Required(ErrorMessage = "El empleado de recepción es requerido")]
        public int EmpleadoRecepcionId { get; set; }

        public int? EmpleadoAsignadoId { get; set; }

        [Required(ErrorMessage = "La descripción del problema es requerida")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string DescripcionProblema { get; set; } = string.Empty;

        public string? Observaciones { get; set; }

        public DateTime? FechaEstimadaEntrega { get; set; }

        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;

        // public PrioridadOrden Prioridad { get; set; } = PrioridadOrden.Media;
    }

    /// <summary>
    /// DTO para actualización de orden de trabajo
    /// </summary>
    public class OrdenTrabajoUpdateDTO : OrdenTrabajoCreateDTO
    {
        [Required]
        public int OrdenTrabajoId { get; set; }

        public DateTime? FechaEntrega { get; set; }
    }

    /// <summary>
    /// DTO completo para lectura de orden de trabajo
    /// </summary>
    public class OrdenTrabajoDTO
    {
        public int OrdenTrabajoId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public EstadoOrden Estado { get; set; }
        public string EstadoDescripcion => Estado.ToString();
        // public PrioridadOrden Prioridad { get; set; }
        // public string PrioridadDescripcion => Prioridad.ToString();
        
        // Cliente
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteTelefono { get; set; } = string.Empty;
        
        // Vehículo
        public int VehiculoId { get; set; }
        public string VehiculoPlaca { get; set; } = string.Empty;
        public string VehiculoDescripcion { get; set; } = string.Empty;
        
        // Empleados
        public int EmpleadoRecepcionId { get; set; }
        public string EmpleadoRecepcionNombre { get; set; } = string.Empty;
        public int? EmpleadoAsignadoId { get; set; }
        public string? EmpleadoAsignadoNombre { get; set; }
        
        // Descripción
        public string DescripcionProblema { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        
        // Cálculos
        public decimal TotalServicios { get; set; }
        public decimal TotalRepuestos { get; set; }
        public decimal Total => TotalServicios + TotalRepuestos;
        
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas
    /// </summary>
    public class OrdenTrabajoListDTO
    {
        public int OrdenTrabajoId { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
        public EstadoOrden Estado { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string VehiculoPlaca { get; set; } = string.Empty;
        public string EmpleadoAsignadoNombre { get; set; } = string.Empty;
        public decimal Total { get; set; }
    }

    /// <summary>
    /// DTO para cambio de estado de orden
    /// </summary>
    public class CambiarEstadoOrdenDTO
    {
        [Required]
        public int OrdenTrabajoId { get; set; }

        [Required]
        public EstadoOrden NuevoEstado { get; set; }

        [Required]
        public int EmpleadoId { get; set; }

        [StringLength(500)]
        public string? Observaciones { get; set; }
    }
}
