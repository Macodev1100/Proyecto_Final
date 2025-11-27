using AutoMapper;
using MotorTechService.Models.DTOs;
using MotorTechService.Models.Entities;

namespace MotorTechService.Mappings
{
    /// <summary>
    /// Perfil de AutoMapper que define las conversiones entre Entidades y DTOs
    /// Centraliza la lógica de mapeo para mantener consistencia
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ===== Cliente Mappings =====
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.TotalVehiculos, 
                    opt => opt.MapFrom(src => src.Vehiculos != null ? src.Vehiculos.Count : 0))
                .ForMember(dest => dest.TotalOrdenes, 
                    opt => opt.MapFrom(src => src.OrdenesTrabajo != null ? src.OrdenesTrabajo.Count : 0))
                .ForMember(dest => dest.Vehiculos, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesTrabajo, opt => opt.Ignore());

            CreateMap<Cliente, ClienteListDTO>()
                .ForMember(dest => dest.NombreCompleto, 
                    opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"))
                .ForMember(dest => dest.TotalVehiculos, 
                    opt => opt.MapFrom(src => src.Vehiculos != null ? src.Vehiculos.Count : 0))
                .ForMember(dest => dest.Vehiculos, opt => opt.Ignore());

            CreateMap<ClienteCreateDTO, Cliente>()
                .ForMember(dest => dest.ClienteId, opt => opt.Ignore())
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true));

            CreateMap<ClienteUpdateDTO, Cliente>()
                .ForMember(dest => dest.FechaRegistro, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.Ignore())
                .ForMember(dest => dest.Vehiculos, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesTrabajo, opt => opt.Ignore());
            
            CreateMap<Cliente, ClienteUpdateDTO>();

            // ===== Vehículo Mappings =====
            CreateMap<Vehiculo, VehiculoDTO>()
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty));

            CreateMap<Vehiculo, VehiculoListDTO>()
                .ForMember(dest => dest.MarcaModelo, 
                    opt => opt.MapFrom(src => $"{src.Marca} {src.Modelo}"))
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty))
                .ForMember(dest => dest.TipoCombustible,
                    opt => opt.MapFrom(src => src.TipoCombustible ?? "N/A"));

            CreateMap<VehiculoCreateDTO, Vehiculo>()
                .ForMember(dest => dest.VehiculoId, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesTrabajo, opt => opt.Ignore());

            CreateMap<VehiculoUpdateDTO, Vehiculo>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesTrabajo, opt => opt.Ignore());

            // ===== OrdenTrabajo Mappings =====
            CreateMap<OrdenTrabajo, OrdenTrabajoDTO>()
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty))
                .ForMember(dest => dest.ClienteTelefono, 
                    opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Telefono : string.Empty))
                .ForMember(dest => dest.VehiculoPlaca, 
                    opt => opt.MapFrom(src => src.Vehiculo != null ? src.Vehiculo.Placa : string.Empty))
                .ForMember(dest => dest.VehiculoDescripcion, 
                    opt => opt.MapFrom(src => src.Vehiculo != null ? $"{src.Vehiculo.Marca} {src.Vehiculo.Modelo} {src.Vehiculo.Anio}" : string.Empty))
                .ForMember(dest => dest.EmpleadoRecepcionNombre, 
                    opt => opt.MapFrom(src => src.EmpleadoRecepcion != null ? $"{src.EmpleadoRecepcion.Nombre} {src.EmpleadoRecepcion.Apellido}" : string.Empty))
                .ForMember(dest => dest.EmpleadoAsignadoNombre, 
                    opt => opt.MapFrom(src => src.EmpleadoAsignado != null ? $"{src.EmpleadoAsignado.Nombre} {src.EmpleadoAsignado.Apellido}" : null))
                .ForMember(dest => dest.TotalServicios, 
                    opt => opt.MapFrom(src => src.Servicios != null ? src.Servicios.Sum(s => s.Precio * s.Cantidad * (1 - s.Descuento / 100)) : 0))
                .ForMember(dest => dest.TotalRepuestos, 
                    opt => opt.MapFrom(src => src.Repuestos != null ? src.Repuestos.Sum(r => r.PrecioUnitario * r.Cantidad * (1 - r.Descuento / 100)) : 0));

            CreateMap<OrdenTrabajo, OrdenTrabajoListDTO>()
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty))
                .ForMember(dest => dest.VehiculoPlaca, 
                    opt => opt.MapFrom(src => src.Vehiculo != null ? src.Vehiculo.Placa : string.Empty))
                .ForMember(dest => dest.EmpleadoAsignadoNombre, 
                    opt => opt.MapFrom(src => src.EmpleadoAsignado != null ? $"{src.EmpleadoAsignado.Nombre} {src.EmpleadoAsignado.Apellido}" : string.Empty))
                .ForMember(dest => dest.Total, 
                    opt => opt.MapFrom(src => 
                        (src.Servicios != null ? src.Servicios.Sum(s => s.Precio * s.Cantidad * (1 - s.Descuento / 100)) : 0) +
                        (src.Repuestos != null ? src.Repuestos.Sum(r => r.PrecioUnitario * r.Cantidad * (1 - r.Descuento / 100)) : 0)));

            CreateMap<OrdenTrabajoCreateDTO, OrdenTrabajo>()
                .ForMember(dest => dest.OrdenTrabajoId, opt => opt.Ignore())
                .ForMember(dest => dest.NumeroOrden, opt => opt.Ignore())
                .ForMember(dest => dest.FechaIngreso, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Vehiculo, opt => opt.Ignore())
                .ForMember(dest => dest.EmpleadoRecepcion, opt => opt.Ignore())
                .ForMember(dest => dest.EmpleadoAsignado, opt => opt.Ignore())
                .ForMember(dest => dest.Servicios, opt => opt.Ignore())
                .ForMember(dest => dest.Repuestos, opt => opt.Ignore())
                .ForMember(dest => dest.HistorialEstados, opt => opt.Ignore())
                .ForMember(dest => dest.Facturas, opt => opt.Ignore());

            CreateMap<OrdenTrabajoUpdateDTO, OrdenTrabajo>()
                .ForMember(dest => dest.FechaIngreso, opt => opt.Ignore())
                .ForMember(dest => dest.NumeroOrden, opt => opt.Ignore())
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Vehiculo, opt => opt.Ignore())
                .ForMember(dest => dest.EmpleadoRecepcion, opt => opt.Ignore())
                .ForMember(dest => dest.EmpleadoAsignado, opt => opt.Ignore())
                .ForMember(dest => dest.Servicios, opt => opt.Ignore())
                .ForMember(dest => dest.Repuestos, opt => opt.Ignore())
                .ForMember(dest => dest.HistorialEstados, opt => opt.Ignore())
                .ForMember(dest => dest.Facturas, opt => opt.Ignore());

            // ===== Empleado Mappings =====
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(dest => dest.OrdenesAsignadas, 
                    opt => opt.MapFrom(src => src.OrdenesAsignadas != null ? src.OrdenesAsignadas.Count : 0));

            CreateMap<Empleado, EmpleadoListDTO>()
                .ForMember(dest => dest.NombreCompleto, 
                    opt => opt.MapFrom(src => $"{src.Nombre} {src.Apellido}"))
                .ForMember(dest => dest.TipoEmpleadoDescripcion, 
                    opt => opt.MapFrom(src => src.TipoEmpleado.ToString()));

            CreateMap<EmpleadoCreateDTO, Empleado>()
                .ForMember(dest => dest.EmpleadoId, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.OrdenesAsignadas, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesRecibidas, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrosTiempo, opt => opt.Ignore());

            CreateMap<EmpleadoUpdateDTO, Empleado>()
                .ForMember(dest => dest.OrdenesAsignadas, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenesRecibidas, opt => opt.Ignore())
                .ForMember(dest => dest.RegistrosTiempo, opt => opt.Ignore());

            // ===== Factura Mappings =====
            CreateMap<Factura, FacturaDTO>()
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty))
                .ForMember(dest => dest.ClienteDocumento, 
                    opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.DocumentoIdentidad : string.Empty))
                .ForMember(dest => dest.OrdenNumero, 
                    opt => opt.MapFrom(src => src.OrdenTrabajo != null ? src.OrdenTrabajo.NumeroOrden : null))
                .ForMember(dest => dest.TotalPagado, 
                    opt => opt.MapFrom(src => src.Pagos != null ? src.Pagos.Sum(p => p.Monto) : 0));

            CreateMap<Factura, FacturaListDTO>()
                .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombre} {src.Cliente.Apellido}" : string.Empty))
                .ForMember(dest => dest.SaldoPendiente, 
                    opt => opt.MapFrom(src => src.Total - (src.Pagos != null ? src.Pagos.Sum(p => p.Monto) : 0)));

            CreateMap<FacturaCreateDTO, Factura>()
                .ForMember(dest => dest.FacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.NumeroFactura, opt => opt.Ignore())
                .ForMember(dest => dest.FechaEmision, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenTrabajo, opt => opt.Ignore())
                .ForMember(dest => dest.Pagos, opt => opt.Ignore());

            CreateMap<FacturaUpdateDTO, Factura>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenTrabajo, opt => opt.Ignore())
                .ForMember(dest => dest.Pagos, opt => opt.Ignore());

            // ===== Repuesto Mappings =====
            CreateMap<Repuesto, RepuestoDTO>()
                .ForMember(dest => dest.PrecioCompra, opt => opt.MapFrom(src => src.PrecioCosto))
                .ForMember(dest => dest.CategoriaNombre, 
                    opt => opt.MapFrom(src => src.CategoriaRepuesto != null ? src.CategoriaRepuesto.Nombre : string.Empty))
                .ForMember(dest => dest.FechaUltimaActualizacion, opt => opt.MapFrom(src => src.FechaCreacion));

            CreateMap<Repuesto, RepuestoListDTO>()
                .ForMember(dest => dest.CategoriaNombre, 
                    opt => opt.MapFrom(src => src.CategoriaRepuesto != null ? src.CategoriaRepuesto.Nombre : string.Empty))
                .ForMember(dest => dest.StockBajo, 
                    opt => opt.MapFrom(src => src.StockActual <= src.StockMinimo));

            CreateMap<RepuestoCreateDTO, Repuesto>()
                .ForMember(dest => dest.RepuestoId, opt => opt.Ignore())
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.PrecioCosto, opt => opt.MapFrom(src => src.PrecioCompra))
                .ForMember(dest => dest.CategoriaRepuesto, opt => opt.Ignore())
                .ForMember(dest => dest.MovimientosInventario, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenTrabajoRepuestos, opt => opt.Ignore());

            CreateMap<RepuestoUpdateDTO, Repuesto>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.Ignore())
                .ForMember(dest => dest.PrecioCosto, opt => opt.MapFrom(src => src.PrecioCompra))
                .ForMember(dest => dest.CategoriaRepuesto, opt => opt.Ignore())
                .ForMember(dest => dest.MovimientosInventario, opt => opt.Ignore())
                .ForMember(dest => dest.OrdenTrabajoRepuestos, opt => opt.Ignore());
        }
    }
}
