using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorTechService.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 1,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4677));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 2,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4680));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 3,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4682));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 4,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4684));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 5,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4685));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 6,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4687));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4560));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4618));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 26, 19, 23, 6, 800, DateTimeKind.Local).AddTicks(4620));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 1,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 2,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 3,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5628));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 4,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5630));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 5,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.UpdateData(
                table: "Configuraciones",
                keyColumn: "ConfiguracionId",
                keyValue: 6,
                column: "FechaModificacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5633));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5508));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5559));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5562));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5564));

            migrationBuilder.UpdateData(
                table: "Servicios",
                keyColumn: "ServicioId",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2025, 11, 19, 14, 29, 12, 110, DateTimeKind.Local).AddTicks(5566));
        }
    }
}
