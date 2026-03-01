using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixRoleSeedModelChangesWarning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "564ebb52-711e-47a0-b592-123f22c72a02");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f992efe1-2b15-4d0a-b9a9-8e656014b28e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13facfcb-31d7-46dc-a095-2fe084d5dfce", "ded0fa7a-ad29-47d6-b124-59c687851c83", "User", "USER" },
                    { "65c1fb98-1e43-4fae-9d2a-e24dc5cdeee2", "b100653c-5a52-4375-91cd-8ac98d39b37b", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13facfcb-31d7-46dc-a095-2fe084d5dfce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65c1fb98-1e43-4fae-9d2a-e24dc5cdeee2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "564ebb52-711e-47a0-b592-123f22c72a02", "ff69ec30-9201-4258-b50d-71518b8f851d", "Admin", "ADMIN" },
                    { "f992efe1-2b15-4d0a-b9a9-8e656014b28e", "6630589a-ee9e-4a68-b8b6-1317f4b35838", "User", "USER" }
                });
        }
    }
}
