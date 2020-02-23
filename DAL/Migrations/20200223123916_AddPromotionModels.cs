using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddPromotionModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromotionMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    PromotionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromotionMappings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionMappings_Promotions_PromotionId",
                        column: x => x.PromotionId,
                        principalTable: "Promotions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CurrencyId",
                table: "Products",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMappings_ProductId",
                table: "PromotionMappings",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMappings_PromotionId",
                table: "PromotionMappings",
                column: "PromotionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                table: "Products",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Currencies_CurrencyId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PromotionMappings");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropIndex(
                name: "IX_Products_CurrencyId",
                table: "Products");
        }
    }
}
