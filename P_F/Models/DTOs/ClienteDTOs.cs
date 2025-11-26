using System.ComponentModel.DataAnnotations;

namespace P_F.Models.DTOs
{
    /// <summary>
    /// DTO para la creación de un nuevo cliente
    /// Incluye validaciones de negocio
    /// </summary>
    public class ClienteCreateDTO
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El documento de identidad es requerido")]
        [StringLength(20, ErrorMessage = "El documento no puede exceder 20 caracteres")]
        public string DocumentoIdentidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }
    }

    /// <summary>
    /// DTO para actualización de cliente
    /// </summary>
    public class ClienteUpdateDTO : ClienteCreateDTO
    {
        [Required]
        public int ClienteId { get; set; }
    }

    /// <summary>
    /// DTO para lectura de cliente con información completa
    /// </summary>
    public class ClienteDTO
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int TotalVehiculos { get; set; }
        public int TotalOrdenes { get; set; }
        public bool Activo { get; set; }
        
        // Colecciones para Details
        public List<VehiculoListDTO>? Vehiculos { get; set; }
        public List<OrdenTrabajoListDTO>? OrdenesTrabajo { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas y referencias
    /// </summary>
    public class ClienteListDTO
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string DocumentoIdentidad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        public int TotalVehiculos { get; set; }
        
        // Para compatibilidad con vistas existentes
        public List<VehiculoListDTO>? Vehiculos { get; set; }
    }
}
