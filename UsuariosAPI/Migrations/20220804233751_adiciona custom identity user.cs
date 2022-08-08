using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosAPI.Migrations
{
    public partial class adicionacustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "b3ae4006-a242-4b9f-aa62-1adc8c508fff");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "f39f38dc-a41b-45a0-a75c-8382b6730581");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50cad748-536b-4587-b128-5b7d64e98c64", "AQAAAAEAACcQAAAAEMT1Q4CAXmP8OCLVAGsfbDO2xhIKMdKNBGmfG41VkE5eDQbbhZ9EtTPg34J0tlPs0g==", "9109c7cd-3093-4946-9731-7e992ed40040" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "c5c6c06c-141d-48ec-8f26-9f7d273261cd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "bdf8c6dd-b7ea-4131-ad38-06c0ac8822fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5541b7f-e6a6-4741-8eb8-820f160892de", "AQAAAAEAACcQAAAAEM8u6xbjYX4V7b2b4wBK3T5PKzTkDVlgmXHrbJ9OZl4YmSxAUlmdJ+9ELb/9t78QlA==", "acf3e8b1-02df-468e-bd15-516f2088812a" });
        }
    }
}
