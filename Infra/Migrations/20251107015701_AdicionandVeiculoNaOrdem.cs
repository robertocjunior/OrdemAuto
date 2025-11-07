using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandVeiculoNaOrdem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nCdVeiculo",
                table: "OrdemServico",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrdemServico_nCdVeiculo",
                table: "OrdemServico",
                column: "nCdVeiculo");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico",
                column: "nCdVeiculo",
                principalTable: "VEICULOS",
                principalColumn: "nCdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico");

            migrationBuilder.DropIndex(
                name: "IX_OrdemServico_nCdVeiculo",
                table: "OrdemServico");

            migrationBuilder.DropColumn(
                name: "nCdVeiculo",
                table: "OrdemServico");
        }
    }
}
