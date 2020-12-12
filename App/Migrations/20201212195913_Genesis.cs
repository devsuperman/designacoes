using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class Genesis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Publicadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Sexo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    DesignadoId = table.Column<int>(type: "int", nullable: false),
                    AjudanteId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designacoes_Publicadores_AjudanteId",
                        column: x => x.AjudanteId,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Designacoes_Publicadores_DesignadoId",
                        column: x => x.DesignadoId,
                        principalTable: "Publicadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_AjudanteId",
                table: "Designacoes",
                column: "AjudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_DesignadoId",
                table: "Designacoes",
                column: "DesignadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designacoes");

            migrationBuilder.DropTable(
                name: "Publicadores");
        }
    }
}
