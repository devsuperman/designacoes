using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class AjudanteNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designacoes_Publicadores_AjudanteId",
                table: "Designacoes");

            migrationBuilder.AlterColumn<int>(
                name: "AjudanteId",
                table: "Designacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Designacoes_Publicadores_AjudanteId",
                table: "Designacoes",
                column: "AjudanteId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designacoes_Publicadores_AjudanteId",
                table: "Designacoes");

            migrationBuilder.AlterColumn<int>(
                name: "AjudanteId",
                table: "Designacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Designacoes_Publicadores_AjudanteId",
                table: "Designacoes",
                column: "AjudanteId",
                principalTable: "Publicadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
