using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExcursionBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2065249e-d5b0-4c1f-b63a-d05a296a357e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53142b32-028d-4338-a680-90bc36ab649e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "935c8c7e-264a-4ad3-b77a-47bad806e042", null, "admin", "admin" },
                    { "f8c0f42b-268d-4b87-83b0-16572c9b5138", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "935c8c7e-264a-4ad3-b77a-47bad806e042");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8c0f42b-268d-4b87-83b0-16572c9b5138");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2065249e-d5b0-4c1f-b63a-d05a296a357e", null, "client", "client" },
                    { "53142b32-028d-4338-a680-90bc36ab649e", null, "admin", "admin" }
                });
        }
    }
}
