using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixConcurrencyStampAndDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13facfcb-31d7-46dc-a095-2fe084d5dfce",
                column: "ConcurrencyStamp",
                value: "e512cefb-cb31-419b-b5ee-8f5b8ec5d1bf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65c1fb98-1e43-4fae-9d2a-e24dc5cdeee2",
                column: "ConcurrencyStamp",
                value: "c4608c7e-b64d-4e92-9366-267320c99bba");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13facfcb-31d7-46dc-a095-2fe084d5dfce",
                column: "ConcurrencyStamp",
                value: "ded0fa7a-ad29-47d6-b124-59c687851c83");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65c1fb98-1e43-4fae-9d2a-e24dc5cdeee2",
                column: "ConcurrencyStamp",
                value: "b100653c-5a52-4375-91cd-8ac98d39b37b");
        }
    }
}
