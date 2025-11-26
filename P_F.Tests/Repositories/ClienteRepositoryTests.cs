using System.Linq.Expressions;

namespace P_F.Tests.Repositories
{
    public class ClienteRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_DeberiaRetornarTodosLosClientesActivos()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var clientesEsperados = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nombre = "Juan", Apellido = "Pérez", Activo = true },
                new Cliente { ClienteId = 2, Nombre = "María", Apellido = "García", Activo = true }
            };
            
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(clientesEsperados);

            // Act
            var resultado = await mockRepo.Object.GetAllAsync();

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            resultado.Should().BeEquivalentTo(clientesEsperados);
        }

        [Fact]
        public async Task GetByIdAsync_ClienteExistente_DeberiaRetornarCliente()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var clienteEsperado = new Cliente 
            { 
                ClienteId = 1, 
                Nombre = "Juan", 
                Apellido = "Pérez", 
                Activo = true 
            };
            
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(clienteEsperado);

            // Act
            var resultado = await mockRepo.Object.GetByIdAsync(1);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().BeEquivalentTo(clienteEsperado);
            resultado!.ClienteId.Should().Be(1);
            resultado.Nombre.Should().Be("Juan");
        }

        [Fact]
        public async Task GetByIdAsync_ClienteNoExistente_DeberiaRetornarNull()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Cliente?)null);

            // Act
            var resultado = await mockRepo.Object.GetByIdAsync(999);

            // Assert
            resultado.Should().BeNull();
        }

        [Fact]
        public async Task AddAsync_DeberiaCrearNuevoCliente()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var nuevoCliente = new Cliente 
            { 
                Nombre = "Pedro", 
                Apellido = "López", 
                DocumentoIdentidad = "12345678",
                Telefono = "555-1234",
                Email = "pedro@email.com",
                Activo = true
            };
            
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Cliente>()))
                .ReturnsAsync((Cliente c) => { c.ClienteId = 1; return c; });

            // Act
            var resultado = await mockRepo.Object.AddAsync(nuevoCliente);

            // Assert
            resultado.Should().NotBeNull();
            resultado.ClienteId.Should().BeGreaterThan(0);
            resultado.Nombre.Should().Be("Pedro");
        }

        [Fact]
        public async Task SearchAsync_DeberiaEncontrarClientesPorTermino()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var clientesEsperados = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nombre = "Juan", Apellido = "Pérez", Activo = true },
                new Cliente { ClienteId = 2, Nombre = "Juana", Apellido = "García", Activo = true }
            };
            
            mockRepo.Setup(r => r.SearchAsync("Juan")).ReturnsAsync(clientesEsperados);

            // Act
            var resultado = await mockRepo.Object.SearchAsync("Juan");

            // Assert
            resultado.Should().NotBeNull();
            resultado.Should().HaveCount(2);
            resultado.All(c => c.Nombre.Contains("Juan")).Should().BeTrue();
        }

        [Fact]
        public async Task GetByDocumentoAsync_DocumentoExistente_DeberiaRetornarCliente()
        {
            // Arrange
            var mockRepo = new Mock<IClienteRepository>();
            var clienteEsperado = new Cliente 
            { 
                ClienteId = 1, 
                Nombre = "Juan", 
                Apellido = "Pérez",
                DocumentoIdentidad = "12345678",
                Activo = true 
            };
            
            mockRepo.Setup(r => r.GetByDocumentoAsync("12345678")).ReturnsAsync(clienteEsperado);

            // Act
            var resultado = await mockRepo.Object.GetByDocumentoAsync("12345678");

            // Assert
            resultado.Should().NotBeNull();
            resultado!.DocumentoIdentidad.Should().Be("12345678");
        }
    }
}
