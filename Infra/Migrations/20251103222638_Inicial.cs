using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PARCEIRO_NEGOCIO",
                columns: table => new
                {
                    nCdParceiro = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sNmParceiro = table.Column<string>(type: "text", nullable: false),
                    eTipo = table.Column<int>(type: "integer", nullable: false),
                    sTelefone = table.Column<string>(type: "text", nullable: false),
                    sEmail = table.Column<string>(type: "text", nullable: false),
                    sCpfCnpj = table.Column<string>(type: "text", nullable: false),
                    bFlAtivo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARCEIRO_NEGOCIO", x => x.nCdParceiro);
                });

            migrationBuilder.CreateTable(
                name: "PECAS",
                columns: table => new
                {
                    nCdPeca = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sNmPeca = table.Column<string>(type: "text", nullable: false),
                    sCor = table.Column<string>(type: "text", nullable: false),
                    sModelo = table.Column<string>(type: "text", nullable: false),
                    tDtAno = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sValor = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PECAS", x => x.nCdPeca);
                });

            migrationBuilder.CreateTable(
                name: "VEICULOS",
                columns: table => new
                {
                    nCdVeiculo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sNmVeiculo = table.Column<string>(type: "text", nullable: false),
                    sCor = table.Column<string>(type: "text", nullable: false),
                    sTipo = table.Column<string>(type: "text", nullable: false),
                    tDtAno = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    sPlaca = table.Column<string>(type: "text", nullable: false),
                    sMecanico = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEICULOS", x => x.nCdVeiculo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARCEIRO_NEGOCIO");

            migrationBuilder.DropTable(
                name: "PECAS");

            migrationBuilder.DropTable(
                name: "VEICULOS");
        }
    }
}
