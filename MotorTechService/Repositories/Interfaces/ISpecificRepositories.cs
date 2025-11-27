using MotorTechService.Models.Entities;

namespace MotorTechService.Repositories
{
    /// <summary>
    /// Interfaz específica para el repositorio de Cliente
    /// Extiende operaciones básicas con consultas especializadas
    /// </summary>
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> GetByDocumentoAsync(string documento);
        Task<IEnumerable<Cliente>> SearchAsync(string searchTerm);
        Task<IEnumerable<Cliente>> GetActivosAsync();
        Task<Cliente?> GetWithVehiculosAsync(int clienteId);
        Task<Cliente?> GetWithOrdenesAsync(int clienteId);
    }

    /// <summary>
    /// Interfaz específica para el repositorio de Vehiculo
    /// </summary>
    public interface IVehiculoRepository : IRepository<Vehiculo>
    {
        Task<Vehiculo?> GetByPlacaAsync(string placa);
        Task<IEnumerable<Vehiculo>> GetByClienteIdAsync(int clienteId);
        Task<Vehiculo?> GetWithClienteAsync(int vehiculoId);
        Task<IEnumerable<Vehiculo>> GetActivosAsync();
    }

    /// <summary>
    /// Interfaz específica para el repositorio de OrdenTrabajo
    /// </summary>
    public interface IOrdenTrabajoRepository : IRepository<OrdenTrabajo>
    {
        Task<IEnumerable<OrdenTrabajo>> GetByEstadoAsync(EstadoOrden estado);
        Task<IEnumerable<OrdenTrabajo>> GetByEmpleadoAsync(int empleadoId);
        Task<IEnumerable<OrdenTrabajo>> GetByClienteAsync(int clienteId);
        Task<IEnumerable<OrdenTrabajo>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<OrdenTrabajo?> GetByNumeroOrdenAsync(string numeroOrden);
        Task<OrdenTrabajo?> GetCompleteAsync(int ordenId);
        Task<string> GenerarNumeroOrdenAsync();
    }

    /// <summary>
    /// Interfaz específica para el repositorio de Empleado
    /// </summary>
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        Task<Empleado?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Empleado>> GetByTipoAsync(TipoEmpleado tipo);
        Task<IEnumerable<Empleado>> GetActivosAsync();
        Task<Empleado?> GetWithOrdenesAsync(int empleadoId);
    }

    /// <summary>
    /// Interfaz específica para el repositorio de Factura
    /// </summary>
    public interface IFacturaRepository : IRepository<Factura>
    {
        Task<Factura?> GetByNumeroFacturaAsync(string numeroFactura);
        Task<IEnumerable<Factura>> GetByClienteAsync(int clienteId);
        Task<IEnumerable<Factura>> GetByEstadoAsync(EstadoFactura estado);
        Task<IEnumerable<Factura>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<Factura?> GetWithPagosAsync(int facturaId);
        Task<string> GenerarNumeroFacturaAsync();
        Task<decimal> GetSaldoPendienteAsync(int facturaId);
    }

    /// <summary>
    /// Interfaz específica para el repositorio de Repuesto (Inventario)
    /// </summary>
    public interface IRepuestoRepository : IRepository<Repuesto>
    {
        Task<IEnumerable<Repuesto>> GetBajoStockAsync();
        Task<IEnumerable<Repuesto>> GetByCategoriaAsync(int categoriaId);
        Task<Repuesto?> GetByCodigoAsync(string codigo);
        Task<IEnumerable<Repuesto>> SearchAsync(string searchTerm);
        Task<bool> ActualizarStockAsync(int repuestoId, int cantidad);
    }

    /// <summary>
    /// Interfaz específica para el repositorio de Servicio
    /// </summary>
    public interface IServicioRepository : IRepository<Servicio>
    {
        Task<IEnumerable<Servicio>> GetByCategoriaAsync(int categoriaId);
        Task<IEnumerable<Servicio>> GetActivosAsync();
        Task<IEnumerable<Servicio>> SearchAsync(string searchTerm);
    }
}
