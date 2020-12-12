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
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Sexo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Designacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PublicadorId = table.Column<int>(type: "int", nullable: true),
                    AjudanteId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataDeRegistro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Designacoes_Pessoa_AjudanteId",
                        column: x => x.AjudanteId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designacoes_Pessoa_PublicadorId",
                        column: x => x.PublicadorId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_AjudanteId",
                table: "Designacoes",
                column: "AjudanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_PublicadorId",
                table: "Designacoes",
                column: "PublicadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Designacoes");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
