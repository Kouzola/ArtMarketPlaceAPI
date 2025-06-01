using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomizationToCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomizationId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CustomizationId",
                table: "CartItems",
                column: "CustomizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Customizations_CustomizationId",
                table: "CartItems",
                column: "CustomizationId",
                principalTable: "Customizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Customizations_CustomizationId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_CustomizationId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "CustomizationId",
                table: "CartItems");
        }
    }
}
