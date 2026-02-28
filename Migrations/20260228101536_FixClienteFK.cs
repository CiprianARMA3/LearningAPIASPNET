using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixClienteFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operazioni_Clienti_ClienteId",
                table: "Operazioni");

            migrationBuilder.DropIndex(
                name: "IX_Operazioni_ClienteId",
                table: "Operazioni");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Operazioni");

            migrationBuilder.CreateIndex(
                name: "IX_Operazioni_IdCliente",
                table: "Operazioni",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Operazioni_Clienti_IdCliente",
                table: "Operazioni",
                column: "IdCliente",
                principalTable: "Clienti",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operazioni_Clienti_IdCliente",
                table: "Operazioni");

            migrationBuilder.DropIndex(
                name: "IX_Operazioni_IdCliente",
                table: "Operazioni");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Operazioni",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operazioni_ClienteId",
                table: "Operazioni",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operazioni_Clienti_ClienteId",
                table: "Operazioni",
                column: "ClienteId",
                principalTable: "Clienti",
                principalColumn: "Id");
        }
    }
}
