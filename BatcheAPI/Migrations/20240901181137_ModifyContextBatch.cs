using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatcheAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContextBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "batch_number",
                table: "batches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_batches_batch_number",
                table: "batches",
                column: "batch_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_batches_batch_number",
                table: "batches");

            migrationBuilder.DropColumn(
                name: "batch_number",
                table: "batches");
        }
    }
}
