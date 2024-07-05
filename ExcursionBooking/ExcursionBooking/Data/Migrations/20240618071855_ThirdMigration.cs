using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExcursionBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Tours_CartItem_TourId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Tours_TourId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_CartItem_TourId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_CategoryId",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_TourId",
                table: "Tours");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ef68659-5c8c-41de-b793-2d4702717b4b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6493b1fd-857e-433f-a7b5-7f8104adad75");

            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CartItem_TourId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CartItem_UserId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TourToCategory_TourId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tours");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourToCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourToCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2065249e-d5b0-4c1f-b63a-d05a296a357e", null, "client", "client" },
                    { "53142b32-028d-4338-a680-90bc36ab649e", null, "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_TourId",
                table: "CartItems",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TourId",
                table: "Orders",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "TourToCategories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2065249e-d5b0-4c1f-b63a-d05a296a357e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53142b32-028d-4338-a680-90bc36ab649e");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartItem_TourId",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartItem_UserId",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tours",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Tours",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourToCategory_TourId",
                table: "Tours",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ef68659-5c8c-41de-b793-2d4702717b4b", null, "admin", "admin" },
                    { "6493b1fd-857e-433f-a7b5-7f8104adad75", null, "client", "client" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CartItem_TourId",
                table: "Tours",
                column: "CartItem_TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_CategoryId",
                table: "Tours",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourId",
                table: "Tours",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Categories_CategoryId",
                table: "Tours",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Tours_CartItem_TourId",
                table: "Tours",
                column: "CartItem_TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Tours_TourId",
                table: "Tours",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
