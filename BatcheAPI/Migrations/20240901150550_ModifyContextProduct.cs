using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatcheAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContextProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Products",
                newName: "products");

            migrationBuilder.RenameIndex(
                name: "IX_Products_product_name",
                table: "products",
                newName: "IX_products_product_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "products",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_products_product_name",
                table: "Products",
                newName: "IX_Products_product_name");
        }
    }
}
