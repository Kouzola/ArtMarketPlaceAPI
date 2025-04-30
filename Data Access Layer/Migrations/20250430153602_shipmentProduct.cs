using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class shipmentProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Shipments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_ProductId",
                table: "Shipments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Products_ProductId",
                table: "Shipments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Products_ProductId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_ProductId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Shipments");
        }
    }
}
