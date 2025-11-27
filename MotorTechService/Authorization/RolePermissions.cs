namespace MotorTechService.Authorization
{
    /// <summary>
    /// Define los roles del sistema
    /// </summary>
    public static class Roles
    {
        public const string Administrador = "Administrador";
        public const string Supervisor = "Supervisor";
        public const string Mecanico = "Mecánico";
        public const string Recepcionista = "Recepcionista";

        public static string[] GetAllRoles() => new[] { Administrador, Supervisor, Mecanico, Recepcionista };
    }

    /// <summary>
    /// Define las políticas de autorización del sistema
    /// </summary>
    public static class Policies
    {
        // Políticas de acceso general
        public const string RequireAdministrador = "RequireAdministrador";
        public const string RequireSupervisor = "RequireSupervisor";
        public const string RequireMecanico = "RequireMecanico";
        public const string RequireRecepcionista = "RequireRecepcionista";

        // Políticas combinadas
        public const string RequireAdminOrSupervisor = "RequireAdminOrSupervisor";
        public const string RequireAdminOrRecepcionista = "RequireAdminOrRecepcionista";
        public const string RequireAnyEmployee = "RequireAnyEmployee";

        // Políticas por módulo
        public const string CanManageClientes = "CanManageClientes";
        public const string CanManageVehiculos = "CanManageVehiculos";
        public const string CanManageOrdenes = "CanManageOrdenes";
        public const string CanViewOrdenes = "CanViewOrdenes";
        public const string CanManageInventario = "CanManageInventario";
        public const string CanViewInventario = "CanViewInventario";
        public const string CanManageEmpleados = "CanManageEmpleados";
        public const string CanManageFacturas = "CanManageFacturas";
        public const string CanViewReportes = "CanViewReportes";
        public const string CanManageSystem = "CanManageSystem";
    }

    /// <summary>
    /// Matriz de permisos por rol y módulo
    /// </summary>
    public static class PermissionMatrix
    {
        // Clientes: Admin, Supervisor, Recepcionista (lectura/escritura), Mecánico (solo lectura)
        public static readonly string[] CanManageClientes = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };
        public static readonly string[] CanViewClientes = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista, Roles.Mecanico };

        // Vehículos: Admin, Supervisor, Recepcionista (lectura/escritura), Mecánico (solo lectura)
        public static readonly string[] CanManageVehiculos = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };
        public static readonly string[] CanViewVehiculos = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista, Roles.Mecanico };

        // Órdenes de Trabajo: Admin y Supervisor (gestión completa), Recepcionista (crear/asignar), Mecánico (actualizar estado sus órdenes)
        public static readonly string[] CanManageOrdenes = { Roles.Administrador, Roles.Supervisor };
        public static readonly string[] CanCreateOrdenes = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };
        public static readonly string[] CanViewOrdenes = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista, Roles.Mecanico };
        public static readonly string[] CanUpdatePropiasOrdenes = { Roles.Mecanico };

        // Inventario/Repuestos: Admin y Supervisor (gestión completa), Recepcionista (consulta y uso en órdenes)
        public static readonly string[] CanManageInventario = { Roles.Administrador, Roles.Supervisor };
        public static readonly string[] CanViewInventario = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };

        // Empleados: Solo Admin y Supervisor
        public static readonly string[] CanManageEmpleados = { Roles.Administrador, Roles.Supervisor };

        // Facturas: Admin, Supervisor, Recepcionista
        public static readonly string[] CanManageFacturas = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };

        // Reportes: Admin, Supervisor (lectura completa), Recepcionista (reportes básicos)
        public static readonly string[] CanViewReportes = { Roles.Administrador, Roles.Supervisor, Roles.Recepcionista };
        public static readonly string[] CanViewAdvancedReportes = { Roles.Administrador, Roles.Supervisor };

        // Configuración del sistema: Solo Admin
        public static readonly string[] CanManageSystem = { Roles.Administrador };
    }
}
