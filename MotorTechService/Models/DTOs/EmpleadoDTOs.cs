using MotorTechService.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace MotorTechService.Models.DTOs
{
    /// <summary>
    /// DTO para creación de empleado
    /// </summary>
    public class EmpleadoCreateDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento es requerido")]
        [StringLength(20)]
        public string DocumentoIdentidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El tipo de empleado es requerido")]
        public TipoEmpleado TipoEmpleado { get; set; }

        [Required(ErrorMessage = "La fecha de contratación es requerida")]
        public DateTime FechaContratacion { get; set; }

        [Required(ErrorMessage = "El salario es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "El salario debe ser mayor a 0")]
        public decimal Salario { get; set; }

        public string? UserId { get; set; }
    }

    /// <summary>
    /// DTO para actualización de empleado
    /// </summary>
    public class EmpleadoUpdateDTO : EmpleadoCreateDTO
    {
        [Required]
        public int EmpleadoId { get; set; }

        public DateTime? FechaTerminacion { get; set; }
    }

    /// <summary>
    /// DTO para lectura de empleado
    /// </summary>
    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public TipoEmpleado TipoEmpleado { get; set; }
        public string TipoEmpleadoDescripcion => TipoEmpleado.ToString();
        public DateTime FechaContratacion { get; set; }
        public DateTime? FechaTerminacion { get; set; }
        public decimal Salario { get; set; }
        public int OrdenesAsignadas { get; set; }
        public bool Activo { get; set; }
        public string? UserId { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas
    /// </summary>
    public class EmpleadoListDTO
    {
        public int EmpleadoId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public TipoEmpleado TipoEmpleado { get; set; }
        public string TipoEmpleadoDescripcion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
