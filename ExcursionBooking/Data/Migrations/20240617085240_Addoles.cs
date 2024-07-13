using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExcursionBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "80b2e07a-339f-4ede-86e6-6c8a935b7e2a", null, "admin", "admin" },
                    { "b15c6dfc-cae2-4dae-8ecd-be16023f2786", null, "client", "client" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80b2e07a-339f-4ede-86e6-6c8a935b7e2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b15c6dfc-cae2-4dae-8ecd-be16023f2786");
        }
    }
}
