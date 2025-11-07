using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_AdicionandVeiculoNaOrdem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico");

            migrationBuilder.AlterColumn<int>(
                name: "nCdVeiculo",
                table: "OrdemServico",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico",
                column: "nCdVeiculo",
                principalTable: "VEICULOS",
                principalColumn: "nCdVeiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico");

            migrationBuilder.AlterColumn<int>(
                name: "nCdVeiculo",
                table: "OrdemServico",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdemServico_VEICULOS_nCdVeiculo",
                table: "OrdemServico",
                column: "nCdVeiculo",
                principalTable: "VEICULOS",
                principalColumn: "nCdVeiculo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
