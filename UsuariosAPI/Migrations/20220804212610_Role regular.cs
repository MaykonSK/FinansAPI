using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class Roleregular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "bdf8c6dd-b7ea-4131-ad38-06c0ac8822fb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "c5c6c06c-141d-48ec-8f26-9f7d273261cd", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5541b7f-e6a6-4741-8eb8-820f160892de", "AQAAAAEAACcQAAAAEM8u6xbjYX4V7b2b4wBK3T5PKzTkDVlgmXHrbJ9OZl4YmSxAUlmdJ+9ELb/9t78QlA==", "acf3e8b1-02df-468e-bd15-516f2088812a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "bf29a04e-4622-4e6e-b3d7-cd6d67965536");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "06fc7543-ddbc-4946-91b3-06230fcce819", "AQAAAAEAACcQAAAAEFgKacLZ+/ik/e7bupqV36HkKry2QT79nC8lz0a3OcJAPiUBSJD7vKmC1OfkoeMroQ==", "e28a49fb-8d04-4073-8008-31307bcffec5" });
        }
    }
}
