using Microsoft.EntityFrameworkCore;
using P_F.Data;
using P_F.Models.Entities;
using P_F.Repositories;

namespace P_F.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoService(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<IEnumerable<Vehiculo>> GetAllAsync()
        {
            return await _vehiculoRepository.GetAllWithIncludesAsync(
                v => v.Activo,
                v => v.Cliente
            );
        }

        public async Task<Vehiculo?> GetByIdAsync(int id)
        {
            return await _vehiculoRepository.GetByIdWithIncludesAsync(
                id,
                v => v.Cliente,
                v => v.OrdenesTrabajo,
                v => v.HistorialMantenimientos
            );
        }

        public async Task<IEnumerable<Vehiculo>> GetByClienteIdAsync(int clienteId)
        {
            return await _vehiculoRepository.GetAllWithIncludesAsync(
                v => v.ClienteId == clienteId && v.Activo,
                v => v.Cliente
            );
        }

        public async Task<Vehiculo> CreateAsync(Vehiculo vehiculo)
        {
            return await _vehiculoRepository.AddAsync(vehiculo);
        }

        public async Task<Vehiculo> UpdateAsync(Vehiculo vehiculo)
        {
            await _vehiculoRepository.UpdateAsync(vehiculo);
            return vehiculo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehiculo = await _vehiculoRepository.GetByIdAsync(id);
            if (vehiculo == null) return false;

            vehiculo.Activo = false;
            await _vehiculoRepository.UpdateAsync(vehiculo);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var vehiculo = await _vehiculoRepository.GetByIdAsync(id);
            return vehiculo != null && vehiculo.Activo;
        }

        public async Task<Vehiculo?> GetByPlacaAsync(string placa)
        {
            return await _vehiculoRepository.GetByPlacaAsync(placa);
        }
    }
}