using Microsoft.EntityFrameworkCore;
using P_F.Data;
using P_F.Models.Entities;

namespace P_F.Repositories
{
    /// <summary>
    /// Repositorio concreto para la entidad Cliente
    /// Implementa consultas especializadas de negocio
    /// </summary>
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Cliente?> GetByDocumentoAsync(string documento)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.DocumentoIdentidad == documento && c.Activo);
        }

        public async Task<IEnumerable<Cliente>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(c => c.Activo && 
                    (c.Nombre.Contains(searchTerm) || 
                     c.Apellido.Contains(searchTerm) || 
                     c.DocumentoIdentidad!.Contains(searchTerm) ||
                     c.Telefono.Contains(searchTerm)))
                .Include(c => c.Vehiculos)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetActivosAsync()
        {
            return await _dbSet
                .Where(c => c.Activo)
                .Include(c => c.Vehiculos)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        public async Task<Cliente?> GetWithVehiculosAsync(int clienteId)
        {
            return await _dbSet
                .Include(c => c.Vehiculos)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.Activo);
        }

        public async Task<Cliente?> GetWithOrdenesAsync(int clienteId)
        {
            return await _dbSet
                .Include(c => c.OrdenesTrabajo)
                    .ThenInclude(o => o.Vehiculo)
                .Include(c => c.Vehiculos)
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId && c.Activo);
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad Vehiculo
    /// </summary>
    public class VehiculoRepository : Repository<Vehiculo>, IVehiculoRepository
    {
        public VehiculoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Vehiculo?> GetByPlacaAsync(string placa)
        {
            return await _dbSet
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Placa == placa && v.Activo);
        }

        public async Task<IEnumerable<Vehiculo>> GetByClienteIdAsync(int clienteId)
        {
            return await _dbSet
                .Where(v => v.ClienteId == clienteId && v.Activo)
                .OrderBy(v => v.Marca)
                .ToListAsync();
        }

        public async Task<Vehiculo?> GetWithClienteAsync(int vehiculoId)
        {
            return await _dbSet
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.VehiculoId == vehiculoId && v.Activo);
        }

        public async Task<IEnumerable<Vehiculo>> GetActivosAsync()
        {
            return await _dbSet
                .Where(v => v.Activo)
                .Include(v => v.Cliente)
                .OrderBy(v => v.Placa)
                .ToListAsync();
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad OrdenTrabajo
    /// </summary>
    public class OrdenTrabajoRepository : Repository<OrdenTrabajo>, IOrdenTrabajoRepository
    {
        public OrdenTrabajoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<OrdenTrabajo>> GetByEstadoAsync(EstadoOrden estado)
        {
            return await _dbSet
                .Where(o => o.Estado == estado && o.Activo)
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .Include(o => o.EmpleadoAsignado)
                .OrderByDescending(o => o.FechaIngreso)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdenTrabajo>> GetByEmpleadoAsync(int empleadoId)
        {
            return await _dbSet
                .Where(o => o.EmpleadoAsignadoId == empleadoId && o.Activo)
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .OrderByDescending(o => o.FechaIngreso)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdenTrabajo>> GetByClienteAsync(int clienteId)
        {
            return await _dbSet
                .Where(o => o.ClienteId == clienteId && o.Activo)
                .Include(o => o.Vehiculo)
                .Include(o => o.EmpleadoAsignado)
                .OrderByDescending(o => o.FechaIngreso)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdenTrabajo>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbSet
                .Where(o => o.FechaIngreso >= fechaInicio && 
                            o.FechaIngreso <= fechaFin && 
                            o.Activo)
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .OrderByDescending(o => o.FechaIngreso)
                .ToListAsync();
        }

        public async Task<OrdenTrabajo?> GetByNumeroOrdenAsync(string numeroOrden)
        {
            return await _dbSet
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .Include(o => o.EmpleadoAsignado)
                .FirstOrDefaultAsync(o => o.NumeroOrden == numeroOrden);
        }

        public async Task<OrdenTrabajo?> GetCompleteAsync(int ordenId)
        {
            return await _dbSet
                .Include(o => o.Cliente)
                .Include(o => o.Vehiculo)
                .Include(o => o.EmpleadoAsignado)
                .Include(o => o.EmpleadoRecepcion)
                .Include(o => o.Servicios)
                    .ThenInclude(s => s.Servicio)
                .Include(o => o.Repuestos)
                    .ThenInclude(r => r.Repuesto)
                .FirstOrDefaultAsync(o => o.OrdenTrabajoId == ordenId);
        }

        public async Task<string> GenerarNumeroOrdenAsync()
        {
            var ultimaOrden = await _dbSet
                .OrderByDescending(o => o.OrdenTrabajoId)
                .FirstOrDefaultAsync();

            var numero = (ultimaOrden?.OrdenTrabajoId ?? 0) + 1;
            return $"ORD-{DateTime.Now:yyyyMM}-{numero:D6}";
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad Empleado
    /// </summary>
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Empleado?> GetByUserIdAsync(string userId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(e => e.UserId == userId && e.Activo);
        }

        public async Task<IEnumerable<Empleado>> GetByTipoAsync(TipoEmpleado tipo)
        {
            return await _dbSet
                .Where(e => e.TipoEmpleado == tipo && e.Activo)
                .OrderBy(e => e.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Empleado>> GetActivosAsync()
        {
            return await _dbSet
                .Where(e => e.Activo)
                .OrderBy(e => e.Nombre)
                .ToListAsync();
        }

        public async Task<Empleado?> GetWithOrdenesAsync(int empleadoId)
        {
            return await _dbSet
                .Include(e => e.OrdenesAsignadas)
                    .ThenInclude(o => o.Cliente)
                .Include(e => e.RegistrosTiempo)
                .FirstOrDefaultAsync(e => e.EmpleadoId == empleadoId && e.Activo);
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad Factura
    /// </summary>
    public class FacturaRepository : Repository<Factura>, IFacturaRepository
    {
        public FacturaRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Factura?> GetByNumeroFacturaAsync(string numeroFactura)
        {
            return await _dbSet
                .Include(f => f.Cliente)
                .Include(f => f.OrdenTrabajo)
                .FirstOrDefaultAsync(f => f.NumeroFactura == numeroFactura);
        }

        public async Task<IEnumerable<Factura>> GetByClienteAsync(int clienteId)
        {
            return await _dbSet
                .Where(f => f.ClienteId == clienteId)
                .Include(f => f.OrdenTrabajo)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> GetByEstadoAsync(EstadoFactura estado)
        {
            return await _dbSet
                .Where(f => f.Estado == estado)
                .Include(f => f.Cliente)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _dbSet
                .Where(f => f.FechaEmision >= fechaInicio && f.FechaEmision <= fechaFin)
                .Include(f => f.Cliente)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<Factura?> GetWithPagosAsync(int facturaId)
        {
            return await _dbSet
                .Include(f => f.Cliente)
                .Include(f => f.OrdenTrabajo)
                .Include(f => f.Pagos)
                .FirstOrDefaultAsync(f => f.FacturaId == facturaId);
        }

        public async Task<string> GenerarNumeroFacturaAsync()
        {
            var ultimaFactura = await _dbSet
                .OrderByDescending(f => f.FacturaId)
                .FirstOrDefaultAsync();

            var numero = (ultimaFactura?.FacturaId ?? 0) + 1;
            return $"FAC-{DateTime.Now:yyyyMM}-{numero:D6}";
        }

        public async Task<decimal> GetSaldoPendienteAsync(int facturaId)
        {
            var factura = await _dbSet
                .Include(f => f.Pagos)
                .FirstOrDefaultAsync(f => f.FacturaId == facturaId);

            if (factura == null) return 0;

            var totalPagado = factura.Pagos?.Sum(p => p.Monto) ?? 0;
            return factura.Total - totalPagado;
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad Repuesto
    /// </summary>
    public class RepuestoRepository : Repository<Repuesto>, IRepuestoRepository
    {
        public RepuestoRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Repuesto>> GetBajoStockAsync()
        {
            return await _dbSet
                .Where(r => r.StockActual <= r.StockMinimo && r.Activo)
                .Include(r => r.CategoriaRepuesto)
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Repuesto>> GetByCategoriaAsync(int categoriaId)
        {
            return await _dbSet
                .Where(r => r.CategoriaRepuestoId == categoriaId && r.Activo)
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        public async Task<Repuesto?> GetByCodigoAsync(string codigo)
        {
            return await _dbSet
                .Include(r => r.CategoriaRepuesto)
                .FirstOrDefaultAsync(r => r.Codigo == codigo && r.Activo);
        }

        public async Task<IEnumerable<Repuesto>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(r => r.Activo && 
                    (r.Nombre.Contains(searchTerm) || 
                     r.Codigo.Contains(searchTerm) ||
                     r.Descripcion!.Contains(searchTerm)))
                .Include(r => r.CategoriaRepuesto)
                .OrderBy(r => r.Nombre)
                .ToListAsync();
        }

        public async Task<bool> ActualizarStockAsync(int repuestoId, int cantidad)
        {
            var repuesto = await _dbSet.FindAsync(repuestoId);
            if (repuesto == null) return false;

            repuesto.StockActual += cantidad;
            // repuesto.FechaUltimaActualizacion = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }

    /// <summary>
    /// Repositorio concreto para la entidad Servicio
    /// </summary>
    public class ServicioRepository : Repository<Servicio>, IServicioRepository
    {
        public ServicioRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Servicio>> GetByCategoriaAsync(int categoriaId)
        {
            return await _dbSet
                .Where(s => s.CategoriaServicioId == categoriaId && s.Activo)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Servicio>> GetActivosAsync()
        {
            return await _dbSet
                .Where(s => s.Activo)
                .Include(s => s.CategoriaServicio)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Servicio>> SearchAsync(string searchTerm)
        {
            return await _dbSet
                .Where(s => s.Activo && 
                    (s.Nombre.Contains(searchTerm) || 
                     s.Descripcion!.Contains(searchTerm)))
                .Include(s => s.CategoriaServicio)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }
    }
}
