using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Migrations
{
    /// <inheritdoc />
    public partial class listaddedtoorderrow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderRowsEntityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderRowsEntityId",
                table: "Products",
                column: "OrderRowsEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderRows_OrderRowsEntityId",
                table: "Products",
                column: "OrderRowsEntityId",
                principalTable: "OrderRows",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderRows_OrderRowsEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderRowsEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderRowsEntityId",
                table: "Products");
        }
    }
}
