using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoAllVoyaje.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbClientes",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "tbTiposPacotes",
                columns: table => new
                {
                    TipoPacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTiposPacotes", x => x.TipoPacoteId);
                });

            migrationBuilder.CreateTable(
                name: "tbPacoteClientes",
                columns: table => new
                {
                    PacoteClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoPacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QtdPessoas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPacoteClientes", x => x.PacoteClienteId);
                    table.ForeignKey(
                        name: "FK_tbPacoteClientes_tbClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "tbClientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbPacoteClientes_tbTiposPacotes_TipoPacoteId",
                        column: x => x.TipoPacoteId,
                        principalTable: "tbTiposPacotes",
                        principalColumn: "TipoPacoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbPacoteViagens",
                columns: table => new
                {
                    PacoteViagemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPacoteViagens", x => x.PacoteViagemId);
                    table.ForeignKey(
                        name: "FK_tbPacoteViagens_tbTiposPacotes_TipoPacoteId",
                        column: x => x.TipoPacoteId,
                        principalTable: "tbTiposPacotes",
                        principalColumn: "TipoPacoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbOpcoesDatas",
                columns: table => new
                {
                    OpcoesDatasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataSaida = table.Column<DateOnly>(type: "date", nullable: false),
                    DataRetorno = table.Column<DateOnly>(type: "date", nullable: false),
                    PacoteViagemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbOpcoesDatas", x => x.OpcoesDatasId);
                    table.ForeignKey(
                        name: "FK_tbOpcoesDatas_tbPacoteViagens_PacoteViagemId",
                        column: x => x.PacoteViagemId,
                        principalTable: "tbPacoteViagens",
                        principalColumn: "PacoteViagemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbOpcoesDatas_PacoteViagemId",
                table: "tbOpcoesDatas",
                column: "PacoteViagemId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPacoteClientes_ClienteId",
                table: "tbPacoteClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPacoteClientes_TipoPacoteId",
                table: "tbPacoteClientes",
                column: "TipoPacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPacoteViagens_TipoPacoteId",
                table: "tbPacoteViagens",
                column: "TipoPacoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbOpcoesDatas");

            migrationBuilder.DropTable(
                name: "tbPacoteClientes");

            migrationBuilder.DropTable(
                name: "tbPacoteViagens");

            migrationBuilder.DropTable(
                name: "tbClientes");

            migrationBuilder.DropTable(
                name: "tbTiposPacotes");
        }
    }
}
