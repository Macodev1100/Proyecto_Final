using Microsoft.EntityFrameworkCore;
using P_F.Models.Entities;

namespace P_F.Data
{
    public static class SeedLargeData
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            Console.WriteLine("Iniciando carga de datos de prueba masivos...");

            // Verificar si ya existen datos suficientes
            if (await context.Clientes.CountAsync() > 50)
            {
                Console.WriteLine("Ya existen datos de prueba suficientes.");
                return;
            }

            var random = new Random();


            // Arrays de datos para generar variedad y realismo
            var nombres = new[] { "Juan", "María", "Pedro", "Ana", "Luis", "Carmen", "Jorge", "Rosa", "Carlos", "Laura",
                                  "Miguel", "Sofia", "Diego", "Elena", "Fernando", "Patricia", "Roberto", "Isabel", "Antonio", "Marta",
                                  "Francisco", "Beatriz", "Javier", "Lucia", "Manuel", "Cristina", "Ricardo", "Gabriela", "Alberto", "Valentina",
                                  "Esteban", "Paula", "Andrés", "Verónica", "Hugo", "Daniela", "Santiago", "Camila", "Sebastián", "Julieta" };

            var apellidos = new[] { "García", "Rodríguez", "González", "Fernández", "López", "Martínez", "Sánchez", "Pérez", "Gómez", "Martín",
                                   "Jiménez", "Ruiz", "Hernández", "Díaz", "Moreno", "Álvarez", "Muñoz", "Romero", "Alonso", "Gutiérrez",
                                   "Navarro", "Torres", "Domínguez", "Vázquez", "Ramos", "Gil", "Ramírez", "Serrano", "Blanco", "Molina",
                                   "Castro", "Ortega", "Delgado", "Ortiz", "Iglesias", "Santos", "Cruz", "Aguilar", "Pascual", "Peña" };

            var marcasVehiculos = new[] { "Toyota", "Honda", "Ford", "Chevrolet", "Nissan", "Mazda", "Hyundai", "Kia", "Volkswagen", "BMW",
                                         "Mercedes-Benz", "Audi", "Subaru", "Jeep", "Dodge", "Ram", "GMC", "Buick", "Cadillac", "Lexus",
                                         "Renault", "Peugeot", "Fiat", "Seat", "Suzuki", "Chery", "BYD", "Geely", "Volvo", "Mini" };

            var modelosVehiculos = new[] { "Corolla", "Civic", "Focus", "Cruze", "Sentra", "Mazda3", "Elantra", "Forte", "Jetta", "320i",
                                          "C-Class", "A4", "Impreza", "Cherokee", "Charger", "1500", "Sierra", "Enclave", "Escalade", "IS",
                                          "Logan", "208", "Panda", "Ibiza", "Swift", "Tiggo", "Song", "Coolray", "XC40", "Cooper" };

            var colores = new[] { "Blanco", "Negro", "Gris", "Plata", "Rojo", "Azul", "Verde", "Amarillo", "Naranja", "Café", "Dorado", "Morado",
                                  "Turquesa", "Borgoña", "Champán", "Oliva", "Celeste", "Granate" };

            var especialidades = new[] { "Motor y Transmisión", "Sistema Eléctrico", "Frenos y Suspensión", "Aire Acondicionado",
                                        "Diagnóstico Computarizado", "Mecánica General", "Carrocería y Pintura", "Electrónica", "Dirección y Ruedas" };

            // Más variedad de servicios
            var nombresServicios = new[] {
                "Cambio de Aceite", "Alineación y Balanceo", "Revisión de Frenos", "Diagnóstico Computarizado", "Cambio de Batería",
                "Reparación de Motor", "Cambio de Amortiguadores", "Reparación de Transmisión", "Cambio de Correa", "Revisión de Suspensión",
                "Cambio de Pastillas de Freno", "Limpieza de Inyectores", "Reparación de Aire Acondicionado", "Cambio de Filtros",
                "Reparación de Sistema Eléctrico", "Cambio de Embrague", "Reparación de Radiador", "Cambio de Bombillas", "Revisión General",
                "Cambio de Neumáticos", "Reparación de Carrocería", "Pintura Parcial", "Pintura Completa", "Cambio de Parabrisas", "Reparación de Puertas"
            };

            var categoriasServicios = new[] { "Mecánica", "Eléctrico", "Carrocería", "Pintura", "Diagnóstico", "Mantenimiento" };

            // Más variedad de repuestos (solo campos que existen en la entidad)
            var nombresRepuestos = new[] {
                "Filtro de Aceite", "Filtro de Aire", "Filtro de Combustible", "Filtro de Cabina", "Aceite Motor 5W-30", "Aceite Motor 10W-40",
                "Aceite Transmisión", "Líquido Frenos", "Pastillas Freno Delanteras", "Pastillas Freno Traseras", "Discos Freno Delanteros",
                "Discos Freno Traseros", "Batería 12V", "Alternador", "Motor Arranque", "Bujías", "Cables Bujías", "Correa Distribución",
                "Correa Accesorios", "Tensor Correa", "Bomba Agua", "Termostato", "Radiador", "Mangueras Radiador", "Ventilador Radiador",
                "Amortiguadores Delanteros", "Amortiguadores Traseros", "Rotulas", "Terminales Dirección", "Embrague Kit Completo", "Volante Motor",
                "Cojinetes", "Retenes", "Sensor Oxígeno", "Sensor MAF", "Sensor Posición Cigüeñal", "Sensor Temperatura", "Faros Delanteros",
                "Focos LED", "Luces Traseras", "Escobillas Limpiaparabrisas", "Neumático 185/65 R15", "Neumático 195/65 R15", "Neumático 205/55 R16",
                "Balanceo Ruedas", "Alineación Computarizada", "Cambio Aceite", "Revisión General", "Escaneo Computadora", "Refrigerante",
                "Limpiador Inyectores"
            };
            // ...el resto del código permanece igual...

            // 1. CREAR 100 CLIENTES
            Console.WriteLine("Creando 100 clientes...");
            var clientes = new List<Cliente>();
            for (int i = 1; i <= 100; i++)
            {
                var cliente = new Cliente
                {
                    Nombre = nombres.Length > 0 ? nombres[random.Next(nombres.Length)] : $"Cliente{i}",
                    Apellido = apellidos.Length > 0 ? apellidos[random.Next(apellidos.Length)] : $"Apellido{i}",
                    DocumentoIdentidad = $"{random.Next(10000000, 99999999)}",
                    Telefono = $"555-{random.Next(1000, 9999)}",
                    Email = $"cliente{i}@email.com",
                    Direccion = $"Calle {random.Next(1, 100)} #{random.Next(1, 50)}-{random.Next(10, 99)}",
                    FechaRegistro = DateTime.Now.AddDays(-random.Next(1, 730)), // Últimos 2 años
                    Activo = true
                };
                clientes.Add(cliente);
            }
            context.Clientes.AddRange(clientes);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {clientes.Count} clientes creados");

            // 2. CREAR 150 VEHÍCULOS (algunos clientes con múltiples vehículos)
            Console.WriteLine("Creando 150 vehículos...");
            var vehiculos = new List<Vehiculo>();
            foreach (var cliente in clientes)
            {
                int cantidadVehiculos = random.Next(1, 3); // 1-2 vehículos por cliente
                for (int v = 0; v < cantidadVehiculos && vehiculos.Count < 150; v++)
                {
                    var vehiculo = new Vehiculo
                    {
                        Placa = $"{(char)random.Next(65, 91)}{(char)random.Next(65, 91)}{(char)random.Next(65, 91)}{random.Next(100, 999)}",
                        Marca = marcasVehiculos.Length > 0 ? marcasVehiculos[random.Next(marcasVehiculos.Length)] : "Marca",
                        Modelo = modelosVehiculos.Length > 0 ? modelosVehiculos[random.Next(modelosVehiculos.Length)] : "Modelo",
                        Anio = random.Next(2010, 2025),
                        Color = colores.Length > 0 ? colores[random.Next(colores.Length)] : "Color",
                        NumeroChasis = $"VIN{Guid.NewGuid().ToString("N").Substring(0, 14).ToUpper()}",
                        NumeroMotor = $"MOT{Guid.NewGuid().ToString("N").Substring(0, 14).ToUpper()}",
                        TipoCombustible = random.Next(2) == 0 ? "Gasolina" : "Diésel",
                        Transmision = random.Next(2) == 0 ? "Manual" : "Automática",
                        Kilometraje = random.Next(5000, 200000),
                        FechaRegistro = DateTime.Now.AddDays(-random.Next(1, 700)),
                        Activo = true,
                        ClienteId = cliente.ClienteId
                    };
                    vehiculos.Add(vehiculo);
                }
            }
            context.Vehiculos.AddRange(vehiculos);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {vehiculos.Count} vehículos creados");

            // 3. CREAR 50 REPUESTOS ADICIONALES
            Console.WriteLine("Creando 50 repuestos...");
            var repuestos = new List<Repuesto>();

            for (int i = 0; i < 50; i++)
            {
                var repuesto = new Repuesto
                {
                    Codigo = $"REP{random.Next(1000, 9999)}",
                    Nombre = nombresRepuestos.Length > 0 ? nombresRepuestos[i % nombresRepuestos.Length] + (i > nombresRepuestos.Length ? $" V{i / nombresRepuestos.Length}" : "") : $"Repuesto{i}",
                    Descripcion = marcasVehiculos.Length > 0 ? $"Repuesto de calidad para {marcasVehiculos[random.Next(marcasVehiculos.Length)]}" : "Repuesto de calidad",
                    CategoriaRepuestoId = random.Next(1, 6),
                    Marca = marcasVehiculos.Length > 0 ? marcasVehiculos[random.Next(marcasVehiculos.Length)] : "Marca",
                    Modelo = modelosVehiculos.Length > 0 ? modelosVehiculos[random.Next(modelosVehiculos.Length)] : "Modelo",
                    PrecioCosto = Math.Round((decimal)(random.Next(10, 500) + random.NextDouble()), 2),
                    PrecioVenta = 0, // Se calculará después
                    PorcentajeGanancia = random.Next(30, 80),
                    StockMinimo = random.Next(5, 15),
                    StockMaximo = random.Next(30, 100),
                    StockActual = random.Next(0, 80),
                    Ubicacion = $"Estante {(char)random.Next(65, 75)}-{random.Next(1, 20)}",
                    Proveedor = $"Proveedor {random.Next(1, 10)}",
                    Activo = true,
                    FechaCreacion = DateTime.Now.AddDays(-random.Next(30, 365))
                };
                repuesto.PrecioVenta = Math.Round(repuesto.PrecioCosto * (1 + repuesto.PorcentajeGanancia / 100), 2);
                repuestos.Add(repuesto);
            }
            context.Repuestos.AddRange(repuestos);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {repuestos.Count} repuestos creados");

            // 4. CREAR 25 EMPLEADOS
            Console.WriteLine("Creando 25 empleados...");
            var empleados = new List<Empleado>();
            for (int i = 1; i <= 25; i++)
            {
                var tipoEmpleado = (TipoEmpleado)random.Next(0, 4);
                var empleado = new Empleado
                {
                    Nombre = nombres.Length > 0 ? nombres[random.Next(nombres.Length)] : $"Empleado{i}",
                    Apellido = apellidos.Length > 0 ? apellidos[random.Next(apellidos.Length)] : $"Apellido{i}",
                    DocumentoIdentidad = $"{random.Next(10000000, 99999999)}",
                    Telefono = $"555-{random.Next(1000, 9999)}",
                    Email = $"empleado{i}@tallerpyf.com",
                    Direccion = $"Avenida {random.Next(1, 50)} #{random.Next(1, 30)}-{random.Next(10, 99)}",
                    TipoEmpleado = tipoEmpleado,
                    Especialidad = tipoEmpleado == TipoEmpleado.Mecanico && especialidades.Length > 0 ? especialidades[random.Next(especialidades.Length)] : null,
                    SalarioHora = Math.Round((decimal)(random.Next(12, 25) + random.NextDouble()), 2),
                    PorcentajeComision = Math.Round((decimal)(random.Next(2, 8) + random.NextDouble()), 2),
                    FechaContratacion = DateTime.Now.AddDays(-random.Next(90, 1095)),
                    FechaTerminacion = null,
                    Activo = true,
                    Observaciones = especialidades.Length > 0 ? $"Empleado con experiencia en {especialidades[random.Next(especialidades.Length)]}" : "Empleado con experiencia"
                };
                empleados.Add(empleado);
            }
            context.Empleados.AddRange(empleados);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {empleados.Count} empleados creados");

            // 5. CREAR 200 ÓRDENES DE TRABAJO
            Console.WriteLine("Creando 200 órdenes de trabajo...");
            var ordenes = new List<OrdenTrabajo>();
            var mecanicos = empleados.Where(e => e.TipoEmpleado == TipoEmpleado.Mecanico).ToList();
            var recepcionistas = empleados.Where(e => e.TipoEmpleado == TipoEmpleado.Recepcionista).ToList();

            var problemasComunes = new[] {
                "Cambio de aceite y filtros", "Revisión general de motor", "Reparación de frenos",
                "Cambio de batería", "Alineación y balanceo", "Reparación sistema eléctrico",
                "Cambio de clutch", "Reparación aire acondicionado", "Cambio de amortiguadores",
                "Reparación transmisión", "Diagnóstico por computadora", "Cambio correa distribución",
                "Reparación radiador", "Cambio pastillas y discos", "Limpieza inyectores"
            };

            for (int i = 1; i <= 200; i++)
            {
                var fechaIngreso = DateTime.Now.AddDays(-random.Next(0, 365));
                var estado = (EstadoOrden)random.Next(0, 5);
                
                var orden = new OrdenTrabajo
                {
                    NumeroOrden = $"ORD-{DateTime.Now.Year}-{i:D4}",
                    FechaIngreso = fechaIngreso,
                    FechaPromesaEntrega = fechaIngreso.AddDays(random.Next(1, 7)),
                    FechaEntrega = estado == EstadoOrden.Completada ? fechaIngreso.AddDays(random.Next(1, 10)) : null,
                    Estado = estado,
                    Prioridad = (Prioridad)random.Next(0, 3),
                    DescripcionProblema = problemasComunes.Length > 0 ? problemasComunes[random.Next(problemasComunes.Length)] : "Problema no especificado",
                    ObservacionesCliente = "Cliente solicita revisión detallada",
                    DiagnosticoTecnico = estado != EstadoOrden.Pendiente ? "Revisión completada, requiere cambios según especificado" : null,
                    RecomendacionesTecnico = estado != EstadoOrden.Pendiente ? "Se recomienda seguimiento en " + random.Next(3000, 5000) + " km" : null,
                    SubTotal = Math.Round((decimal)(random.Next(100, 1500) + random.NextDouble()), 2),
                    Impuestos = 0,
                    Descuento = Math.Round((decimal)(random.Next(0, 100) + random.NextDouble()), 2),
                    Total = 0,
                    RequiereAutorizacion = random.Next(5) == 0,
                    AutorizadoPorCliente = true,
                    FechaAutorizacion = fechaIngreso.AddHours(random.Next(1, 24)),
                    Activo = true,
                    ClienteId = clientes.Count > 0 ? clientes[random.Next(clientes.Count)].ClienteId : 0,
                    VehiculoId = vehiculos.Count > 0 ? vehiculos[random.Next(vehiculos.Count)].VehiculoId : 0,
                    EmpleadoAsignadoId = mecanicos.Count > 0 ? mecanicos[random.Next(mecanicos.Count)].EmpleadoId : null,
                    EmpleadoRecepcionId = recepcionistas.Count > 0 ? recepcionistas[random.Next(recepcionistas.Count)].EmpleadoId : null
                };
                
                orden.Impuestos = Math.Round(orden.SubTotal * 0.12m, 2);
                orden.Total = orden.SubTotal + orden.Impuestos - orden.Descuento;
                ordenes.Add(orden);
            }
            context.OrdenesTrabajo.AddRange(ordenes);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {ordenes.Count} órdenes de trabajo creadas");

            // 6. CREAR SERVICIOS PARA LAS ÓRDENES
            Console.WriteLine("Agregando servicios a órdenes...");
            var servicios = await context.Servicios.ToListAsync();
            var ordenesServicios = new List<OrdenTrabajoServicio>();
            
            foreach (var orden in ordenes.Take(150)) // Primeras 150 órdenes
            {
                int cantServicios = random.Next(1, 4);
                for (int i = 0; i < cantServicios; i++)
                {
                    if (servicios.Count == 0) continue;
                    var servicio = servicios[random.Next(servicios.Count)];
                    var ordenServicio = new OrdenTrabajoServicio
                    {
                        OrdenTrabajoId = orden.OrdenTrabajoId,
                        ServicioId = servicio.ServicioId,
                        Precio = servicio.PrecioBase * (1 + (decimal)(random.Next(-10, 20) / 100.0)),
                        Cantidad = random.Next(1, 3),
                        Descuento = random.Next(0, 10),
                        Observaciones = "Servicio estándar",
                        Completado = orden.Estado == EstadoOrden.Completada,
                        FechaInicio = orden.Estado != EstadoOrden.Pendiente ? orden.FechaIngreso.AddHours(1) : null,
                        FechaFin = orden.Estado == EstadoOrden.Completada ? orden.FechaEntrega : null
                    };
                    ordenesServicios.Add(ordenServicio);
                }
            }
            context.OrdenTrabajoServicios.AddRange(ordenesServicios);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {ordenesServicios.Count} servicios agregados a órdenes");

            // 7. CREAR REPUESTOS PARA LAS ÓRDENES
            Console.WriteLine("Agregando repuestos a órdenes...");
            var ordenesRepuestos = new List<OrdenTrabajoRepuesto>();
            
            foreach (var orden in ordenes.Take(120)) // Primeras 120 órdenes
            {
                int cantRepuestos = random.Next(1, 5);
                for (int i = 0; i < cantRepuestos; i++)
                {
                    if (repuestos.Count == 0) continue;
                    var repuesto = repuestos[random.Next(repuestos.Count)];
                    var ordenRepuesto = new OrdenTrabajoRepuesto
                    {
                        OrdenTrabajoId = orden.OrdenTrabajoId,
                        RepuestoId = repuesto.RepuestoId,
                        Cantidad = random.Next(1, 4),
                        PrecioUnitario = repuesto.PrecioVenta,
                        Descuento = random.Next(0, 15),
                        Observaciones = "Repuesto original",
                        Entregado = orden.Estado == EstadoOrden.Completada
                    };
                    ordenesRepuestos.Add(ordenRepuesto);
                }
            }
            context.OrdenTrabajoRepuestos.AddRange(ordenesRepuestos);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {ordenesRepuestos.Count} repuestos agregados a órdenes");

            // 8. CREAR FACTURAS PARA ÓRDENES COMPLETADAS
            Console.WriteLine("Creando facturas...");
            var ordenesCompletadas = ordenes.Where(o => o.Estado == EstadoOrden.Completada).ToList();
            var facturas = new List<Factura>();
            
            foreach (var orden in ordenesCompletadas.Take(80))
            {
                var empleadoId = orden.EmpleadoRecepcionId;
                if (!empleadoId.HasValue && empleados.Count > 0)
                {
                    empleadoId = empleados.First().EmpleadoId;
                }
                var factura = new Factura
                {
                    NumeroFactura = $"FAC-{DateTime.Now.Year}-{facturas.Count + 1:D4}",
                    FechaEmision = orden.FechaEntrega ?? DateTime.Now,
                    FechaVencimiento = (orden.FechaEntrega ?? DateTime.Now).AddDays(15),
                    Estado = (EstadoFactura)random.Next(0, 3),
                    SubTotal = orden.SubTotal,
                    Impuestos = orden.Impuestos,
                    Descuento = orden.Descuento,
                    Total = orden.Total,
                    Observaciones = "Factura generada automáticamente",
                    ClienteId = orden.ClienteId,
                    OrdenTrabajoId = orden.OrdenTrabajoId,
                    EmpleadoId = empleadoId ?? 0 // 0 if no empleados available
                };
                facturas.Add(factura);
            }
            context.Facturas.AddRange(facturas);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {facturas.Count} facturas creadas");

            // 9. CREAR REGISTROS DE TIEMPO
            Console.WriteLine("Creando registros de tiempo...");
            var registrosTiempo = new List<RegistroTiempo>();
            
            foreach (var orden in ordenes.Where(o => o.Estado != EstadoOrden.Pendiente && o.Estado != EstadoOrden.Cancelada).Take(100))
            {
                if (orden.EmpleadoAsignadoId.HasValue)
                {
                    var empleado = empleados.FirstOrDefault(e => e.EmpleadoId == orden.EmpleadoAsignadoId.Value);
                    var fechaInicio = orden.FechaIngreso.AddHours(random.Next(1, 3));
                    var minutos = random.Next(60, 480); // 1-8 horas
                    
                    var registro = new RegistroTiempo
                    {
                        EmpleadoId = orden.EmpleadoAsignadoId.Value,
                        OrdenTrabajoId = orden.OrdenTrabajoId,
                        FechaInicio = fechaInicio,
                        FechaFin = orden.Estado == EstadoOrden.Completada ? fechaInicio.AddMinutes(minutos) : null,
                        MinutosTrabajados = orden.Estado == EstadoOrden.Completada ? minutos : 0,
                        Descripcion = "Trabajo en orden",
                        CostoHora = empleado?.SalarioHora,
                        CostoTotal = empleado != null ? Math.Round(empleado.SalarioHora * (minutos / 60.0m), 2) : 0
                    };
                    registrosTiempo.Add(registro);
                }
            }
            context.RegistrosTiempo.AddRange(registrosTiempo);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {registrosTiempo.Count} registros de tiempo creados");

            // 10. CREAR MOVIMIENTOS DE INVENTARIO
            Console.WriteLine("Creando movimientos de inventario...");
            var movimientos = new List<MovimientoInventario>();
            
            foreach (var repuesto in repuestos.Take(30))
            {
                // Entrada inicial
                movimientos.Add(new MovimientoInventario
                {
                    RepuestoId = repuesto.RepuestoId,
                    TipoMovimiento = TipoMovimiento.Entrada,
                    Cantidad = repuesto.StockActual + random.Next(10, 50),
                    StockAnterior = 0,
                    StockNuevo = repuesto.StockActual + random.Next(10, 50),
                    Costo = repuesto.PrecioCosto * (repuesto.StockActual + random.Next(10, 50)),
                    Motivo = "Compra inicial",
                    DocumentoReferencia = $"COMP-{random.Next(1000, 9999)}",
                    FechaMovimiento = DateTime.Now.AddDays(-random.Next(30, 180)),
                    EmpleadoId = empleados.Count > 0 ? empleados.FirstOrDefault()?.EmpleadoId : null
                });

                // Algunas salidas
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    movimientos.Add(new MovimientoInventario
                    {
                        RepuestoId = repuesto.RepuestoId,
                        TipoMovimiento = TipoMovimiento.Salida,
                        Cantidad = random.Next(1, 5),
                        StockAnterior = repuesto.StockActual,
                        StockNuevo = repuesto.StockActual - random.Next(1, 5),
                        Costo = null,
                        Motivo = "Venta",
                        DocumentoReferencia = $"ORD-{random.Next(1000, 9999)}",
                        FechaMovimiento = DateTime.Now.AddDays(-random.Next(1, 30)),
                        EmpleadoId = empleados.Count > 0 ? empleados.FirstOrDefault()?.EmpleadoId : null
                    });
                }
            }
            context.MovimientosInventario.AddRange(movimientos);
            await context.SaveChangesAsync();
            Console.WriteLine($"✓ {movimientos.Count} movimientos de inventario creados");

            Console.WriteLine("\n========================================");
            Console.WriteLine("RESUMEN DE DATOS CREADOS:");
            Console.WriteLine("========================================");
            Console.WriteLine($"✓ {clientes.Count} Clientes");
            Console.WriteLine($"✓ {vehiculos.Count} Vehículos");
            Console.WriteLine($"✓ {repuestos.Count} Repuestos");
            Console.WriteLine($"✓ {empleados.Count} Empleados");
            Console.WriteLine($"✓ {ordenes.Count} Órdenes de Trabajo");
            Console.WriteLine($"✓ {ordenesServicios.Count} Servicios en Órdenes");
            Console.WriteLine($"✓ {ordenesRepuestos.Count} Repuestos en Órdenes");
            Console.WriteLine($"✓ {facturas.Count} Facturas");
            Console.WriteLine($"✓ {registrosTiempo.Count} Registros de Tiempo");
            Console.WriteLine($"✓ {movimientos.Count} Movimientos de Inventario");
            Console.WriteLine("========================================");
            Console.WriteLine("✅ Carga masiva completada exitosamente!");
        }
    }
}
