using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientBooker.API.Migrations
{
    /// <inheritdoc />
    public partial class IndexCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "DateTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments");
        }
    }
}
