using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class SituacaoDaDesignacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Situacao",
                table: "Designacoes",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Designacoes");
        }
    }
}
