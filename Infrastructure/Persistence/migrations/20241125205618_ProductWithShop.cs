using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ProductWithShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_ShopId",
                table: "product",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_shop_ShopId",
                table: "product",
                column: "ShopId",
                principalTable: "shop",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_shop_ShopId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_ShopId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "product");
        }
    }
}
