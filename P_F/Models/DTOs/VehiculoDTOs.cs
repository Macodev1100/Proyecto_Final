using System.ComponentModel.DataAnnotations;

namespace P_F.Models.DTOs
{
    /// <summary>
    /// DTO para creación de vehículo
    /// </summary>
    public class VehiculoCreateDTO
    {
        [Required(ErrorMessage = "La placa es requerida")]
        [StringLength(10, ErrorMessage = "La placa no puede exceder 10 caracteres")]
        public string Placa { get; set; } = string.Empty;

        [Required(ErrorMessage = "La marca es requerida")]
        [StringLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Required(ErrorMessage = "El modelo es requerido")]
        [StringLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El año es requerido")]
        [Range(1900, 2100, ErrorMessage = "Año inválido")]
        public int Año { get; set; }

        [StringLength(20)]
        public string? Color { get; set; }

        [StringLength(50)]
        public string? NumeroMotor { get; set; }

        [StringLength(50)]
        public string? NumeroChasis { get; set; }

        public int? Kilometraje { get; set; }

        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClienteId { get; set; }
    }

    /// <summary>
    /// DTO para actualización de vehículo
    /// </summary>
    public class VehiculoUpdateDTO : VehiculoCreateDTO
    {
        [Required]
        public int VehiculoId { get; set; }
    }

    /// <summary>
    /// DTO para lectura de vehículo
    /// </summary>
    public class VehiculoDTO
    {
        public int VehiculoId { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Año { get; set; }
        public string? Color { get; set; }
        public string? NumeroMotor { get; set; }
        public string? NumeroChasis { get; set; }
        public int? Kilometraje { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string DescripcionCompleta => $"{Marca} {Modelo} {Año} - {Placa}";
        public bool Activo { get; set; }
    }

    /// <summary>
    /// DTO simplificado para listas
    /// </summary>
    public class VehiculoListDTO
    {
        public int VehiculoId { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string MarcaModelo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public string? Color { get; set; }
        public string? TipoCombustible { get; set; }
        public int? Kilometraje { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
    }
}
