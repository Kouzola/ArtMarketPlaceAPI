using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class shipmentProduct1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductShipment",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ShipmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShipment", x => new { x.ProductsId, x.ShipmentsId });
                    table.ForeignKey(
                        name: "FK_ProductShipment_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShipment_Shipments_ShipmentsId",
                        column: x => x.ShipmentsId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShipment_ShipmentsId",
                table: "ProductShipment",
                column: "ShipmentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductShipment");

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
    }
}
