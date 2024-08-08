using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace inventory.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _882024727 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("494def42-f12d-4726-81c4-9a963a3db66a"), "Niki" },
                    { new Guid("930284b0-2862-4373-9880-d8faa3e0dfd1"), "Ehsan" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("494def42-f12d-4726-81c4-9a963a3db66a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("930284b0-2862-4373-9880-d8faa3e0dfd1"));
        }
    }
}
