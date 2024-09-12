using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_category_of_product_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_storage_products_ProductId",
                table: "storage");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "storage",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_storage_ProductId",
                table: "storage",
                newName: "IX_storage_product_id");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "storage",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category_of_product",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "products_fk",
                table: "products",
                column: "CategoryId",
                principalTable: "category_of_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "product_fk",
                table: "storage",
                column: "product_id",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "products_fk",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "product_fk",
                table: "storage");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "storage",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_storage_product_id",
                table: "storage",
                newName: "IX_storage_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "storage",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category_of_product",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_category_of_product_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "category_of_product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_storage_products_ProductId",
                table: "storage",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
