namespace MotorTechService.Tests.Services
{
    public class ClienteServiceTests
    {
        private readonly Mock<IClienteRepository> _mockClienteRepo;
        private readonly ClienteService _clienteService;

        public ClienteServiceTests()
        {
            _mockClienteRepo = new Mock<IClienteRepository>();
            _clienteService = new ClienteService(_mockClienteRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeberiaRetornarTodosLosClientes()
        {
            // Arrange
            var clientesEsperados = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nombre = "Juan", Apellido = "Pérez", Activo = true },
                new Cliente { ClienteId = 2, Nombre = "María", Apellido = "García", Activo = true }
            };
            
            _mockClienteRepo.Setup(r => r.GetActivosAsync())
                .ReturnsAsync(clientesEsperados);

            // Act
            var resultado = await _clienteService.GetAllAsync();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            _mockClienteRepo.Verify(r => r.GetActivosAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ClienteExistente_DeberiaRetornarCliente()
        {
            // Arrange
            var clienteEsperado = new Cliente 
            { 
                ClienteId = 1, 
                Nombre = "Juan", 
                Apellido = "Pérez", 
                Activo = true,
                Vehiculos = new List<Vehiculo>(),
                OrdenesTrabajo = new List<OrdenTrabajo>()
            };
            
            _mockClienteRepo.Setup(r => r.GetByIdWithIncludesAsync(
                1,
                It.IsAny<System.Linq.Expressions.Expression<Func<Cliente, object>>[]>()))
                .ReturnsAsync(clienteEsperado);

            // Act
            var resultado = await _clienteService.GetByIdAsync(1);

            // Assert
            resultado.Should().NotBeNull();
            resultado!.ClienteId.Should().Be(1);
            resultado.Nombre.Should().Be("Juan");
        }

        [Fact]
        public async Task CreateAsync_ClienteValido_DeberiaCrearCliente()
        {
            // Arrange
            var nuevoCliente = new Cliente 
            { 
                Nombre = "Pedro", 
                Apellido = "López", 
                DocumentoIdentidad = "12345678",
                Telefono = "555-1234",
                Email = "pedro@email.com",
                Activo = true
            };
            
            _mockClienteRepo.Setup(r => r.AddAsync(It.IsAny<Cliente>()))
                .ReturnsAsync((Cliente c) => { c.ClienteId = 1; return c; });

            // Act
            var resultado = await _clienteService.CreateAsync(nuevoCliente);

            // Assert
            resultado.Should().NotBeNull();
            resultado.ClienteId.Should().BeGreaterThan(0);
            _mockClienteRepo.Verify(r => r.AddAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ClienteValido_DeberiaActualizarCliente()
        {
            // Arrange
            var clienteActualizado = new Cliente 
            { 
                ClienteId = 1,
                Nombre = "Juan Actualizado", 
                Apellido = "Pérez", 
                DocumentoIdentidad = "12345678",
                Telefono = "555-1234",
                Email = "juan@email.com",
                Activo = true
            };
            
            _mockClienteRepo.Setup(r => r.UpdateAsync(It.IsAny<Cliente>()))
                .Returns(Task.CompletedTask);

            // Act
            var resultado = await _clienteService.UpdateAsync(clienteActualizado);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nombre.Should().Be("Juan Actualizado");
            _mockClienteRepo.Verify(r => r.UpdateAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ClienteExistente_DeberiaEliminarLogicamente()
        {
            // Arrange
            var cliente = new Cliente 
            { 
                ClienteId = 1,
                Nombre = "Juan", 
                Apellido = "Pérez",
                Activo = true
            };
            
            _mockClienteRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cliente);
            _mockClienteRepo.Setup(r => r.UpdateAsync(It.IsAny<Cliente>())).Returns(Task.CompletedTask);

            // Act
            var resultado = await _clienteService.DeleteAsync(1);

            // Assert
            resultado.Should().BeTrue();
            cliente.Activo.Should().BeFalse();
            _mockClienteRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
            _mockClienteRepo.Verify(r => r.UpdateAsync(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ClienteNoExistente_DeberiaRetornarFalse()
        {
            // Arrange
            _mockClienteRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Cliente?)null);

            // Act
            var resultado = await _clienteService.DeleteAsync(999);

            // Assert
            resultado.Should().BeFalse();
            _mockClienteRepo.Verify(r => r.UpdateAsync(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public async Task SearchAsync_DeberiaLlamarAlRepositorio()
        {
            // Arrange
            var termino = "Juan";
            var clientesEsperados = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nombre = "Juan", Apellido = "Pérez", Activo = true }
            };
            
            _mockClienteRepo.Setup(r => r.SearchAsync(termino)).ReturnsAsync(clientesEsperados);

            // Act
            var resultado = await _clienteService.SearchAsync(termino);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(1);
            _mockClienteRepo.Verify(r => r.SearchAsync(termino), Times.Once);
        }

        [Fact]
        public async Task ExistsAsync_ClienteExistente_DeberiaRetornarTrue()
        {
            // Arrange
            var cliente = new Cliente { ClienteId = 1, Activo = true };
            _mockClienteRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cliente);

            // Act
            var resultado = await _clienteService.ExistsAsync(1);

            // Assert
            resultado.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsAsync_ClienteNoExistente_DeberiaRetornarFalse()
        {
            // Arrange
            _mockClienteRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Cliente?)null);

            // Act
            var resultado = await _clienteService.ExistsAsync(999);

            // Assert
            resultado.Should().BeFalse();
        }
    }
}
