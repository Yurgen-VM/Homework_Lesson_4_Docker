using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "product_name",
                table: "category_of_product",
                newName: "category_name");

            migrationBuilder.RenameIndex(
                name: "IX_category_of_product_product_name",
                table: "category_of_product",
                newName: "IX_category_of_product_category_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "category_of_product",
                newName: "product_name");

            migrationBuilder.RenameIndex(
                name: "IX_category_of_product_category_name",
                table: "category_of_product",
                newName: "IX_category_of_product_product_name");
        }
    }
}
