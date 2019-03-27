using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudWebStore.Migrations
{
    public partial class FourthCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShoppingCartItem",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Product",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "ShoppingCartItem",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Product",
                newName: "Id");
        }
    }
}
