using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class DataInsert2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Assiettes, bols ou tasses en céramiques.", "Poterie" },
                    { 2, "Bijoux en or ou argent fait maison.", "Bijoux" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "PostalCode", "Street", "Active", "CartId", "Email", "FirstName", "LastName", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "AdminCity", "AdminLand", "12345", "123 Admin St", true, 0, "admin@example.com", "admin", "admin", "AC9126E7F5A5CF1564991D85C03CB1C2", "Admin", "admin" },
                    { 2, "Paris", "France", "7500", "123 Rue de Paris", true, 0, "jean@example.com", "Jean", "Bon", "5FCF306D2BA332CDCC0EDE5C8158138D", "Customer", "Jean" },
                    { 3, "Lyon", "France", "69000", "456 Rue de Lyon", true, 0, "paul@example.com", "Paul", "Sevran", "33073B61D3610D08BBEF298767FE5572", "Artisan", "Paul" },
                    { 4, "Paris", "France", "10000", "Rue des Combattants 14", true, 0, "julien.degol@gmail.com", "Julien", "De Gaulle", "2562122D9A3F07BEB424CAE6CBB45ECE", "Customer", "Julien1" },
                    { 5, "Bordeaux", "France", "25600", "Rue des Potiers", true, 0, "a.dupont@gmail.com", "Adrien", "Dupont", "FA6E8A721B9885D39DCC052976ED5CD8", "Artisan", "Adrien" },
                    { 6, "Bordeaux", "France", "23503", "Rue des Livreurs", true, 0, "g.latour@gmail.com", "Gabin", "Latour", "6314BAF29B5CCD2038277303DA8FE59B", "Delivery", "Gabin" },
                    { 7, "Marseille", "France", "13000", "789 Rue de Marseille", true, 0, "kevin@deliveryman.com", "Kevin", "Statam", "125EA6D54A96CBFCF5144EE10A10F672", "Delivery", "Kevin" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Code", "CustomerId", "OrderDate", "ShippingOption", "Status" },
                values: new object[] { 1, "4e0cc3da6c204423", 4, new DateTime(2025, 6, 8, 13, 41, 27, 63, DateTimeKind.Unspecified).AddTicks(3333), "FAST", "PENDING" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ArtisanId", "Available", "CategoryId", "Description", "Image", "Name", "Price", "Reference", "ReservedStock", "Stock" },
                values: new object[,]
                {
                    { 1, 3, true, 1, "Bol en céramique solide", "46494bf1ef2343bcaf6ef3846296a4e5.jpg", "Bol en céramique", 19.989999999999998, "PAPS-1-82BD9C", 0, 5 },
                    { 2, 3, true, 1, "Assiette Creuse en céramique.", "aa1b85d03b774fb18d00ad1a7287f268.jpg", "Assiette Creuse", 10.0, "PAPS-2-F8E0D5", 0, 10 },
                    { 3, 3, true, 1, "Assiette en céramique.", "b2e82282131c46bb9f91dd22f078e6da.jpg", "Assiette", 15.0, "PAPS-3-3B66A5", 0, 5 },
                    { 4, 5, true, 2, "Collier celte de l'antiquité", "e875c773f7624e078a4258cf5c6ed26b.jpeg", "Collier celte", 49.990000000000002, "ADAD-0-EFE65F", 0, 9 },
                    { 5, 5, true, 2, "Un beau bracelet de fleur en or.", "c74aa49fdeec41b69fe993de0fdfc46e.jpg", "Bracelet de Fleur", 15.0, "ADAD-1-4110F7", 0, 10 }
                });

            migrationBuilder.InsertData(
                table: "Customizations",
                columns: new[] { "Id", "Description", "Name", "Price", "ProductId" },
                values: new object[,]
                {
                    { 1, "Couleur de votre choix.", "Couleur personnalisé", 5.9900000000000002, 1 },
                    { 2, "Taille de votre choix.", "Taille personnalisé", 10.0, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "IsValidatedByArtisan", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[] { 1, false, 1, 4, 1, 49.990000000000002 });

            migrationBuilder.InsertData(
                table: "OrderStatusPerStatus",
                columns: new[] { "ArtisanId", "OrderId", "Status" },
                values: new object[] { 5, 1, 0 });

            migrationBuilder.InsertData(
                table: "PaymentDetails",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "PaymentMethod" },
                values: new object[] { 1, 49.990000000000002, 1, new DateTime(2025, 6, 8, 15, 41, 30, 605, DateTimeKind.Unspecified).AddTicks(2851), "BANCONTACT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customizations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderStatusPerStatus",
                keyColumns: new[] { "ArtisanId", "OrderId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "PaymentDetails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
