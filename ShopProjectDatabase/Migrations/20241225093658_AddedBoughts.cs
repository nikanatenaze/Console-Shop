using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProjectDatabase.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoughts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boughts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boughts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boughts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoughtItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoughtId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoughtItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoughtItems_Boughts_BoughtId",
                        column: x => x.BoughtId,
                        principalTable: "Boughts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoughtItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoughtItems_BoughtId",
                table: "BoughtItems",
                column: "BoughtId");

            migrationBuilder.CreateIndex(
                name: "IX_BoughtItems_ProductId",
                table: "BoughtItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Boughts_AccountId",
                table: "Boughts",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoughtItems");

            migrationBuilder.DropTable(
                name: "Boughts");
        }
    }
}
