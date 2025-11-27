# MotorTechService.Tests

Proyecto de pruebas unitarias para el sistema MotorTechService.

```
MotorTechService.Tests/
├── Repositories/
|   └── ClienteRepositoryTests.cs      # Pruebas de repositorios
├── Services/
|   └── ClienteServiceTests.cs         # Pruebas de servicios
├── MotorTechService.Tests.csproj      # Archivo de proyecto
└── Usings.cs                         # Importaciones globales
```

##  Dependencias

- **xUnit**: Framework de pruebas
- **Moq**: Framework para mocking
- **FluentAssertions**: Aserciones fluidas y legibles
- **EF Core InMemory**: Base de datos en memoria para pruebas

##  Ejecutar Pruebas

### Desde Visual Studio
1. Abrir Test Explorer (Test > Test Explorer)
2. Hacer clic en "Run All"

### Desde línea de comandos
```cmd
cd MotorTechService.Tests
dotnet test
```

### Con cobertura de código
```cmd
dotnet test /p:CollectCoverage=true
```

##  Ejemplos de Pruebas

### Repositorio Tests
```csharp
[Fact]
public async Task GetByIdAsync_ClienteExistente_DeberiaRetornarCliente()
{
    // Arrange
    var mockRepo = new Mock<IClienteRepository>();
    var clienteEsperado = new Cliente { ClienteId = 1, Nombre = "Juan" };
    mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(clienteEsperado);

    // Act
    var resultado = await mockRepo.Object.GetByIdAsync(1);

    // Assert
    resultado.Should().NotBeNull();
    resultado!.ClienteId.Should().Be(1);
}
```

### Service Tests
```csharp
[Fact]
public async Task CreateAsync_ClienteValido_DeberiaCrearCliente()
{
    // Arrange
    var mockRepo = new Mock<IClienteRepository>();
    var service = new ClienteService(mockRepo.Object);
    var cliente = new Cliente { Nombre = "Juan" };

    // Act
    var resultado = await service.CreateAsync(cliente);

    // Assert
    mockRepo.Verify(r => r.AddAsync(It.IsAny<Cliente>()), Times.Once);
}
```

##  Cobertura de Pruebas

### ClienteRepositoryTests
-  GetAllAsync
-  GetByIdAsync (existente y no existente)
-  AddAsync
-  SearchAsync
-  GetByDocumentoAsync

### ClienteServiceTests
-  GetAllAsync
-  GetByIdAsync
-  CreateAsync
-  UpdateAsync
-  DeleteAsync (soft delete)
-  ExistsAsync
-  SearchAsync

##  Patrones de Pruebas Utilizados

### AAA Pattern (Arrange-Act-Assert)
Todas las pruebas siguen el patrón AAA:
- **Arrange**: Configurar el contexto y dependencias
- **Act**: Ejecutar la operación a probar
- **Assert**: Verificar el resultado

### Mocking
Uso de **Moq** para crear objetos simulados (mocks):
```csharp
var mockRepo = new Mock<IClienteRepository>();
mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(cliente);
```

### Fluent Assertions
Aserciones expresivas y legibles:
```csharp
resultado.Should().NotBeNull();
resultado.Should().HaveCount(2);
resultado!.ClienteId.Should().Be(1);
```

##  Convenciones de Nombres

### Nombre de métodos de prueba
```
[Método]_[Escenario]_[ResultadoEsperado]
```

Ejemplos:
- `GetByIdAsync_ClienteExistente_DeberiaRetornarCliente`
- `DeleteAsync_ClienteNoExistente_DeberiaRetornarFalse`
- `CreateAsync_ClienteValido_DeberiaCrearCliente`

##  Agregar Más Pruebas

### Para un nuevo repositorio:
1. Crear archivo en `Repositories/[Nombre]RepositoryTests.cs`
2. Probar cada método del repositorio
3. Incluir casos exitosos y de error

### Para un nuevo servicio:
1. Crear archivo en `Services/[Nombre]ServiceTests.cs`
2. Mockear dependencias (repositorios)
3. Probar lógica de negocio
4. Verificar llamadas a repositorios

##  Beneficios

 **Confianza**: Pruebas automatizan verificación
 **Refactorización**: Cambios seguros con pruebas
 **Documentación**: Pruebas documentan comportamiento
 **Regresiones**: Detectan errores temprano
 **Diseño**: Fuerzan diseño testeable (SOLID)

---

**Nota**: Este proyecto de pruebas demuestra testabilidad de la arquitectura implementada, separación de concerns mediante Repository Pattern, y uso de Dependency Injection para facilitar el mocking.
