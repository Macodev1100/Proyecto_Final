using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P_F.Models.DTOs;
using P_F.Models.Entities;
using P_F.Services;
using P_F.Authorization;

namespace P_F.Controllers
{
    [Authorize(Policy = Policies.CanViewOrdenes)]
    public class ClientesController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly IVehiculoService _vehiculoService;
        private readonly IMapper _mapper;

        public ClientesController(IClienteService clienteService, IVehiculoService vehiculoService, IMapper mapper)
        {
            _clienteService = clienteService;
            _vehiculoService = vehiculoService;
            _mapper = mapper;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.GetAllAsync();
            var clientesDto = _mapper.Map<IEnumerable<ClienteListDTO>>(clientes);
            
            // Mapear vehículos manualmente para cada cliente
            foreach (var clienteDto in clientesDto)
            {
                var cliente = clientes.FirstOrDefault(c => c.ClienteId == clienteDto.ClienteId);
                if (cliente?.Vehiculos != null)
                {
                    clienteDto.Vehiculos = _mapper.Map<List<VehiculoListDTO>>(cliente.Vehiculos);
                }
            }
            
            return View(clientesDto);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            var clienteDto = _mapper.Map<ClienteDTO>(cliente);
            
            // Mapear colecciones manualmente
            if (cliente.Vehiculos != null)
            {
                clienteDto.Vehiculos = _mapper.Map<List<VehiculoListDTO>>(cliente.Vehiculos);
            }
            if (cliente.OrdenesTrabajo != null)
            {
                clienteDto.OrdenesTrabajo = _mapper.Map<List<OrdenTrabajoListDTO>>(cliente.OrdenesTrabajo);
            }
            
            return View(clienteDto);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            var clienteDto = new ClienteCreateDTO();
            return View(clienteDto);
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteCreateDTO clienteDto)
        {
            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);
                await _clienteService.CreateAsync(cliente);
                TempData["Success"] = "Cliente creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDto);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);
            if (cliente == null)
                return NotFound();

            var clienteDto = _mapper.Map<ClienteUpdateDTO>(cliente);
            return View(clienteDto);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteUpdateDTO clienteDto)
        {
            if (id != clienteDto.ClienteId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);
                await _clienteService.UpdateAsync(cliente);
                TempData["Success"] = "Cliente actualizado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(clienteDto);
        }

        // POST: Clientes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clienteService.DeleteAsync(id);
            if (result)
                TempData["Success"] = "Cliente eliminado exitosamente.";
            else
                TempData["Error"] = "No se pudo eliminar el cliente.";

            return RedirectToAction(nameof(Index));
        }

        // AJAX: Buscar clientes
        [HttpGet]
        public async Task<IActionResult> Buscar(string termino)
        {
            var clientes = await _clienteService.SearchAsync(termino ?? "");
            var resultado = clientes.Select(c => new
            {
                id = c.ClienteId,
                nombre = $"{c.Nombre} {c.Apellido}",
                documento = c.DocumentoIdentidad,
                telefono = c.Telefono,
                email = c.Email
            });
            return Json(resultado);
        }
    }
}