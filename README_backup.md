# MotorTechService - Sistema de Gestión para Taller Mecánico

## Descripción General
MotorTechService es un sistema integral de gestión para talleres mecánicos desarrollado en ASP.NET Core 8 MVC con Entity Framework Core y SQL Server. Incluye funcionalidades completas para administrar clientes, vehículos, órdenes de trabajo, inventario, empleados, facturación y reportes.

## Características Principales

###  Gestión de Vehículos y Clientes
- Registro completo de clientes con información de contacto
- Gestión de vehículos asociados a clientes
- Historial de servicios por vehículo
- Búsqueda avanzada y filtros

###  Órdenes de Trabajo
- Creación y seguimiento de órdenes de trabajo
- Estados: Pendiente, En Proceso, En Espera, Completada, Cancelada
- Asignación de mecánicos y servicios
- Cálculo automático de costos
- Historial completo de cambios

###  Control de Inventario
- Gestión de repuestos y materiales
- Control de stock con alertas de stock bajo
- Movimientos de inventario (entradas/salidas)
- Proveedores y compras
- Reportes de inventario crítico

###  Gestión de Empleados
- Registro de empleados con especialidades
- Seguimiento de productividad
- Asignación a órdenes de trabajo
- Reportes de rendimiento

###  Facturación
- Generación automática de facturas
- Múltiples métodos de pago
- Descuentos y promociones
- Reportes de ventas
- Exportación a PDF

###  Centro de Reportes
- Dashboard ejecutivo con métricas clave
- Reportes de ventas por período
- Productividad de empleados
- Clientes frecuentes
- Inventario crítico
- Órdenes por estado
- Exportación a PDF de todos los reportes

###  Sistema de Autenticación y Autorización
- Roles: Administrador, Gerente, Mecánico, Recepcionista
- Permisos granulares por funcionalidad
- Gestión de usuarios
- Inicialización automática del sistema

###  Generación de PDFs
- Facturas profesionales
- Órdenes de trabajo
- Reportes ejecutivos
- Documentos con formato corporativo

###  Sistema de Notificaciones
- Alertas en tiempo real
- Notificaciones de stock bajo
- Recordatorios de mantenimiento
- Mensajes del sistema

## Tecnologías Utilizadas

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core** - ORM para base de datos
- **SQL Server** - Base de datos relacional
- **ASP.NET Core Identity** - Autenticación y autorización

### Frontend
- **Bootstrap 5** - Framework CSS responsive
- **Font Awesome** - Iconografía
- **jQuery** - JavaScript library
- **Chart.js** - Gráficos y visualizaciones

### Librerías Adicionales
- **iTextSharp** - Generación de PDFs
- **Microsoft.AspNetCore.Mvc** - Controladores MVC
- **System.Text.Json** - Serialización JSON

## Estructura del Proyecto

```
MotorTechService/
├── Controllers/          # Controladores MVC
├── Models/              # Modelos de datos
├── Services/            # Lógica de negocio
├── Data/               # Contexto de base de datos
├── Views/              # Vistas Razor
├── wwwroot/            # Archivos estáticos
├── Attributes/         # Atributos personalizados
└── Properties/         # Configuración
```

## Configuración Inicial

### 1. Requisitos del Sistema
- .NET 8.0 SDK
- SQL Server Express o superior
- Visual Studio 2022 o VS Code

### 2. Configuración de Base de Datos
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=TallerMecanicoDb;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

### 3. Instalación de Dependencias
```bash
dotnet restore
dotnet build
```

### 4. Inicialización
```bash
dotnet run
```

El sistema creará automáticamente:
- Base de datos y tablas
- Roles de usuario
- Usuario administrador por defecto: `admin@tallerpyf.com` / `Admin123!`

## Roles y Permisos

### Administrador
- Acceso completo al sistema
- Gestión de usuarios y roles
- Todos los reportes y funcionalidades

### Gerente
- Reportes ejecutivos
- Facturación completa
- Gestión de inventario
- Órdenes de trabajo
- Gestión de empleados

### Mecánico
- Órdenes de trabajo asignadas
- Consulta de vehículos
- Lectura de inventario

### Recepcionista
- Crear/editar órdenes de trabajo
- Gestión de vehículos y clientes
- Consulta de facturación

## URLs Principales

- **Dashboard:** `/Home/Index`
- **Clientes:** `/Clientes`
- **Vehículos:** `/Vehiculos`
- **Órdenes:** `/OrdenesTrabajo`
- **Inventario:** `/Repuestos`
- **Empleados:** `/Empleados`
- **Facturación:** `/Facturas`
- **Reportes:** `/Reportes`
- **Usuarios:** `/Usuarios`

## Características Destacadas

### Dashboard Inteligente
- Métricas en tiempo real
- Gráficos interactivos
- Indicadores de rendimiento
- Alertas importantes

### Búsqueda Global
- Búsqueda en tiempo real
- Filtros avanzados
- Resultados paginados

### Responsive Design
- Compatible con dispositivos móviles
- Interfaz adaptativa
- Experiencia de usuario optimizada

### Exportación de Datos
- PDFs profesionales
- Reportes ejecutivos
- Documentos oficiales

## Mantenimiento

### Logs del Sistema
Los logs se almacenan automáticamente y incluyen:
- Errores de aplicación
- Transacciones importantes
- Accesos de usuarios
- Cambios de datos

### Backup Automático
Se recomienda configurar backup automático de la base de datos SQL Server.

### Actualizaciones
```bash
dotnet build --configuration Release
dotnet publish --configuration Release
```

## Soporte

Para soporte técnico o consultas sobre el sistema, contactar al equipo de desarrollo.

---

**Desarrollado para MotorTechService**
*Sistema completo de gestión empresarial*