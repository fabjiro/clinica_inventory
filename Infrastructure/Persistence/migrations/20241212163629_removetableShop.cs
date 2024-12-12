using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class removetableShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_shop_ShopId",
                table: "user");

            migrationBuilder.DropTable(
                name: "shop");

            migrationBuilder.DropTable(
                name: "shop_type");

            migrationBuilder.DropIndex(
                name: "IX_user_ShopId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShopId",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shop_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("a484a778-a4f4-4606-aac5-b47db6705612")),
                    ShopTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    MinStockProducts = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shop_image_LogoId",
                        column: x => x.LogoId,
                        principalTable: "image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shop_shop_type_ShopTypeId",
                        column: x => x.ShopTypeId,
                        principalTable: "shop_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_ShopId",
                table: "user",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_LogoId",
                table: "shop",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_ShopTypeId",
                table: "shop",
                column: "ShopTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_shop_ShopId",
                table: "user",
                column: "ShopId",
                principalTable: "shop",
                principalColumn: "Id");
        }
    }
}
