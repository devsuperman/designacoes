using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class FuncoesDeSubstituicao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EhSubstituicao",
                table: "Designacoes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MotivoDaSubstituicao",
                table: "Designacoes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubstituicaoId",
                table: "Designacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Designacoes_SubstituicaoId",
                table: "Designacoes",
                column: "SubstituicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designacoes_Designacoes_SubstituicaoId",
                table: "Designacoes",
                column: "SubstituicaoId",
                principalTable: "Designacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designacoes_Designacoes_SubstituicaoId",
                table: "Designacoes");

            migrationBuilder.DropIndex(
                name: "IX_Designacoes_SubstituicaoId",
                table: "Designacoes");

            migrationBuilder.DropColumn(
                name: "EhSubstituicao",
                table: "Designacoes");

            migrationBuilder.DropColumn(
                name: "MotivoDaSubstituicao",
                table: "Designacoes");

            migrationBuilder.DropColumn(
                name: "SubstituicaoId",
                table: "Designacoes");
        }
    }
}
