using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class ImpedirIrmaoDeFazerPartes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ImpedidoDeFazerPartes",
                table: "Publicadores",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpedidoDeFazerPartes",
                table: "Publicadores");
        }
    }
}
