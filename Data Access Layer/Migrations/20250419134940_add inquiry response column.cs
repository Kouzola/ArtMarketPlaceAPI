using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addinquiryresponsecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customization_Product_ProductId",
                table: "Customization");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Users_CustomerId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Product_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_Order_OrderId",
                table: "PaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Users_ArtisanId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_CustomerId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Order_OrderId",
                table: "Shipment");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipment_Users_DeliveryPartnerId",
                table: "Shipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipment",
                table: "Shipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customization",
                table: "Customization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Shipment",
                newName: "Shipments");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "PaymentDetail",
                newName: "PaymentDetails");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "Customization",
                newName: "Customizations");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Shipment_OrderId",
                table: "Shipments",
                newName: "IX_Shipments_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Shipment_DeliveryPartnerId",
                table: "Shipments",
                newName: "IX_Shipments_DeliveryPartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_CustomerId",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Reference",
                table: "Products",
                newName: "IX_Products_Reference");

            migrationBuilder.RenameIndex(
                name: "IX_Product_Image",
                table: "Products",
                newName: "IX_Products_Image");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ArtisanId",
                table: "Products",
                newName: "IX_Products_ArtisanId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetail_OrderId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_Code",
                table: "Orders",
                newName: "IX_Orders_Code");

            migrationBuilder.RenameIndex(
                name: "IX_Customization_ProductId",
                table: "Customizations",
                newName: "IX_Customizations_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Name",
                table: "Categories",
                newName: "IX_Categories_Name");

            migrationBuilder.AddColumn<string>(
                name: "ArtisanResponse",
                table: "Inquiry",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipments",
                table: "Shipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customizations",
                table: "Customizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customizations_Products_ProductId",
                table: "Customizations",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Orders_OrderId",
                table: "PaymentDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_ArtisanId",
                table: "Products",
                column: "ArtisanId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Users_DeliveryPartnerId",
                table: "Shipments",
                column: "DeliveryPartnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customizations_Products_ProductId",
                table: "Customizations");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Orders_OrderId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_ArtisanId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Orders_OrderId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Users_DeliveryPartnerId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipments",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customizations",
                table: "Customizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ArtisanResponse",
                table: "Inquiry");

            migrationBuilder.RenameTable(
                name: "Shipments",
                newName: "Shipment");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "PaymentDetails",
                newName: "PaymentDetail");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderProduct");

            migrationBuilder.RenameTable(
                name: "Customizations",
                newName: "Customization");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipment",
                newName: "IX_Shipment_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Shipments_DeliveryPartnerId",
                table: "Shipment",
                newName: "IX_Shipment_DeliveryPartnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "Review",
                newName: "IX_Review_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "Review",
                newName: "IX_Review_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Reference",
                table: "Product",
                newName: "IX_Product_Reference");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Image",
                table: "Product",
                newName: "IX_Product_Image");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Product",
                newName: "IX_Product_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ArtisanId",
                table: "Product",
                newName: "IX_Product_ArtisanId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_OrderId",
                table: "PaymentDetail",
                newName: "IX_PaymentDetail_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Code",
                table: "Order",
                newName: "IX_Order_Code");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Customizations_ProductId",
                table: "Customization",
                newName: "IX_Customization_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_Name",
                table: "Category",
                newName: "IX_Category_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipment",
                table: "Shipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customization",
                table: "Customization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customization_Product_ProductId",
                table: "Customization",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Users_CustomerId",
                table: "Order",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Order_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Product_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_Order_OrderId",
                table: "PaymentDetail",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Category_CategoryId",
                table: "Product",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Users_ArtisanId",
                table: "Product",
                column: "ArtisanId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Product_ProductId",
                table: "Review",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_CustomerId",
                table: "Review",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Order_OrderId",
                table: "Shipment",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipment_Users_DeliveryPartnerId",
                table: "Shipment",
                column: "DeliveryPartnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
