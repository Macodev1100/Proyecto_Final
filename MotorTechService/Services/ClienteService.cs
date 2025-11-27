using Microsoft.EntityFrameworkCore;
using MotorTechService.Data;
using MotorTechService.Models.Entities;
using MotorTechService.Repositories;

namespace MotorTechService.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetActivosAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdWithIncludesAsync(
                id,
                c => c.Vehiculos,
                c => c.OrdenesTrabajo
            );
        }

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            return await _clienteRepository.AddAsync(cliente);
        }

        public async Task<Cliente> UpdateAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
            return cliente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null) return false;

            cliente.Activo = false;
            await _clienteRepository.UpdateAsync(cliente);
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            return cliente != null && cliente.Activo;
        }

        public async Task<IEnumerable<Cliente>> SearchAsync(string searchTerm)
        {
            return await _clienteRepository.SearchAsync(searchTerm);
        }

        public async Task<Cliente?> GetByDocumentoAsync(string documento)
        {
            return await _clienteRepository.GetByDocumentoAsync(documento);
        }
    }
}