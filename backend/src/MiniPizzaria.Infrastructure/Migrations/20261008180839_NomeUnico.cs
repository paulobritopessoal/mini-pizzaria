using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPizzaria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomeUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_Nome",
                table: "Pizzas",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pizzas_Nome",
                table: "Pizzas");
        }
    }
}
