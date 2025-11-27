# Sistema de Autorización por Roles - Taller P&F

## Roles del Sistema

El sistema implementa **4 roles principales** con diferentes niveles de acceso:

### 1.  Administrador
**Acceso:** Total y sin restricciones

-  Gestión completa de **todos los módulos**
-  Gestión de **usuarios y roles**
-  Configuración del **sistema**
-  Acceso a **reportes avanzados**
-  **Empleados**: Crear, editar, eliminar
-  **Inventario**: Control total
-  **Facturación**: Control total

**Credenciales de prueba:**
- Email: `admin@tallerpyf.com`
- Contraseña: `Admin123!`

---

### 2.  Supervisor
**Acceso:** Gestión operativa completa (sin configuración del sistema)

-  **Clientes y Vehículos**: Gestión completa
-  **Órdenes de Trabajo**: Control total (crear, asignar, modificar, cerrar)
-  **Empleados**: Ver, crear, editar (gestión de personal)
-  **Inventario**: Control total (agregar, editar, movimientos)
-  **Facturación**: Gestión completa
-  **Reportes**: Acceso a reportes avanzados
-  **Administración del sistema**: No puede modificar configuraciones críticas
-  **Gestión de usuarios**: No puede crear/modificar usuarios

**Credenciales de prueba:**
- Email: `supervisor@tallerpyf.com`
- Contraseña: `Supervisor123!`

---

### 3.  Recepcionista
**Acceso:** Operaciones de atención al cliente y facturación

-  **Clientes**: Gestión completa (crear, editar, consultar)
-  **Vehículos**: Gestión completa (registrar, editar, consultar)
-  **Órdenes de Trabajo**: Crear y consultar (no puede cerrar ni eliminar)
-  **Facturación**: Gestión completa (generar facturas, registrar pagos)
-  **Inventario**: Consulta (ver stock, usar en órdenes)
-  **Reportes**: Reportes básicos (ventas, clientes)
-  **Empleados**: No puede gestionar empleados
-  **Inventario**: No puede agregar/editar repuestos
-  **Administración**: Sin acceso

**Credenciales de prueba:**
- Email: `recepcionista@tallerpyf.com`
- Contraseña: `Recepcionista123!`

---

### 4.  Mecánico
**Acceso:** Operaciones de taller (órdenes asignadas)

-  **Órdenes de Trabajo**: Ver y actualizar **solo sus órdenes asignadas**
-  **Clientes y Vehículos**: Consulta (solo lectura)
-  **Registros de Tiempo**: Registrar horas trabajadas
-  **Clientes**: No puede crear ni editar
-  **Vehículos**: No puede registrar ni editar
-  **Inventario**: Sin acceso
-  **Facturación**: Sin acceso
-  **Reportes**: Sin acceso
-  **Empleados**: Sin acceso
-  **Administración**: Sin acceso

**Credenciales de prueba:**
- Email: `mecanico@tallerpyf.com`
- Contraseña: `Mecanico123!`

---

## Matriz de Permisos por Módulo

| Módulo | Administrador | Supervisor | Recepcionista | Mecánico |
|--------|--------------|------------|---------------|----------|
| **Dashboard** | Completo | Completo | Básico | Básico |
| **Clientes** | CRUD completo | CRUD completo | CRUD completo | Solo lectura |
| **Vehículos** | CRUD completo | CRUD completo | CRUD completo | Solo lectura |
| **Órdenes de Trabajo** | Control total | Control total | Crear/Consultar | Sus órdenes |
| **Inventario** | Control total | Control total | Consultar | Sin acceso |
| **Empleados** | CRUD completo | CRUD completo | Sin acceso | Sin acceso |
| **Facturación** | Control total | Control total | Control total | Sin acceso |
| **Reportes** | Avanzados | Avanzados | Básicos | Sin acceso |
| **Administración** | Control total | Sin acceso | Sin acceso | Sin acceso |

---

## Implementación Técnica

### Políticas de Autorización

El sistema utiliza **ASP.NET Core Authorization Policies** para controlar el acceso:

```csharp
// Ejemplos de políticas configuradas
Policies.CanManageClientes      // Admin, Supervisor, Recepcionista
Policies.CanManageOrdenes       // Admin, Supervisor
Policies.CanViewOrdenes         // Todos los roles
Policies.CanManageInventario    // Admin, Supervisor
Policies.CanViewInventario      // Admin, Supervisor, Recepcionista
Policies.CanManageEmpleados     // Admin, Supervisor
Policies.CanManageFacturas      // Admin, Supervisor, Recepcionista
Policies.CanViewReportes        // Admin, Supervisor, Recepcionista
Policies.CanManageSystem        // Solo Admin
```

### Tag Helpers Personalizados

Se han implementado Tag Helpers para controlar la visibilidad en las vistas:

```razor
<!-- Mostrar solo para roles específicos -->
<div asp-roles="Administrador,Supervisor">
    Contenido solo para Admin y Supervisor
</div>

<!-- Mostrar solo si cumple con la política -->
<li asp-policy="CanManageClientes">
    <a asp-controller="Clientes" asp-action="Create">Nuevo Cliente</a>
</li>
```

### Atributos en Controladores

Los controladores están protegidos con atributos de autorización:

```csharp
[Authorize(Policy = Policies.CanManageEmpleados)]
public class EmpleadosController : Controller
{
    // Solo Admin y Supervisor pueden acceder
}

[Authorize(Policy = Policies.CanViewOrdenes)]
public class OrdenesTrabajoController : Controller
{
    // Todos los roles autenticados pueden ver
    
    [Authorize(Policy = Policies.CanManageOrdenes)]
    public async Task<IActionResult> Create()
    {
        // Solo Admin y Supervisor pueden crear
    }
}
```

---

## Seguridad y Mejores Prácticas

###  Implementado

1. **Principio de Mínimo Privilegio**: Cada rol tiene solo los permisos necesarios
2. **Separación de Responsabilidades**: Roles claramente definidos por función
3. **Autorización en Múltiples Capas**:
   - Controladores protegidos con `[Authorize]`
   - Acciones específicas con políticas
   - Vistas con Tag Helpers
4. **Menú Dinámico**: Solo muestra opciones permitidas al usuario
5. **Redirección Inteligente**: Usuarios autenticados van directamente al Dashboard

###  Recomendaciones Futuras

1. **Auditoría**: Implementar logging de acciones por usuario
2. **Permisos Granulares**: Sistema de permisos más detallado si es necesario
3. **Gestión de Roles en UI**: Interfaz para asignar/modificar roles (actualmente solo Admin)
4. **Two-Factor Authentication**: Para cuentas de Administrador
5. **Sesiones y Tokens**: Implementar expiración y renovación de sesiones

---

## Flujo de Trabajo por Rol

### Escenario 1: Nueva Orden de Trabajo

1. **Recepcionista**:
   - Cliente llega al taller
   - Busca/crea cliente en el sistema
   - Registra vehículo si es nuevo
   - Crea orden de trabajo con descripción del problema

2. **Supervisor**:
   - Revisa órdenes pendientes
   - Asigna mecánico apropiado
   - Aprueba presupuesto si es necesario

3. **Mecánico**:
   - Ve sus órdenes asignadas
   - Actualiza progreso y estado
   - Registra tiempo trabajado

4. **Supervisor/Admin**:
   - Cierra orden completada
   - Genera factura

5. **Recepcionista**:
   - Emite factura al cliente
   - Registra pago

### Escenario 2: Gestión de Inventario

1. **Supervisor/Admin**:
   - Revisa stock bajo
   - Crea orden de compra
   - Registra entrada de repuestos

2. **Recepcionista**:
   - Consulta disponibilidad para órdenes
   - Asigna repuestos a órdenes de trabajo

3. **Mecánico**:
   - No tiene acceso directo (solicita a supervisor/recepcionista)

---

## Pruebas de Acceso

Para probar el sistema de roles:

1. **Inicia sesión con diferentes usuarios** y verifica:
   - Opciones visibles en el menú
   - Acceso a módulos específicos
   - Botones de acción disponibles

2. **Intenta acceder directamente a URLs restringidas**:
   - Debería redirigir a "Access Denied" o "Login"

3. **Verifica la matriz de permisos** con cada rol

---

## Soporte

Para modificar roles o permisos:
- Archivo: `Authorization/RolePermissions.cs`
- Configuración: `Program.cs` (sección de políticas)
- Inicialización: `Data/DbInitializer.cs`

