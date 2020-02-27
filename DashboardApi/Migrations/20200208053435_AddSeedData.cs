using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DashboardApi.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "State" },
                values: new object[,]
                {
                    { 1, "mary@gmail.com", "Mary", "CA" },
                    { 2, "john@gmail.com", "John", "NY" }
                });

            migrationBuilder.InsertData(
                table: "Servers",
                columns: new[] { "Id", "IsOnline", "Name" },
                values: new object[,]
                {
                    { 1, true, "Dev" },
                    { 2, true, "Preprod" },
                    { 3, true, "Prod" },
                    { 4, false, "QA" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Completed", "CustomerId", "Placed", "Total" },
                values: new object[] { 1, new DateTime(2019, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2019, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.99m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Completed", "CustomerId", "Placed", "Total" },
                values: new object[] { 3, null, 1, new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.99m });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Completed", "CustomerId", "Placed", "Total" },
                values: new object[] { 2, new DateTime(2018, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2018, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.99m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Servers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
