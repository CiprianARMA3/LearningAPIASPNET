using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "564ebb52-711e-47a0-b592-123f22c72a02", "ff69ec30-9201-4258-b50d-71518b8f851d", "Admin", "ADMIN" },
                    { "f992efe1-2b15-4d0a-b9a9-8e656014b28e", "6630589a-ee9e-4a68-b8b6-1317f4b35838", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "564ebb52-711e-47a0-b592-123f22c72a02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f992efe1-2b15-4d0a-b9a9-8e656014b28e");
        }
    }
}
