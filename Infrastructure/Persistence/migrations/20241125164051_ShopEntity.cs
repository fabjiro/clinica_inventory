using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ShopEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "shop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MinStockProducts = table.Column<int>(type: "integer", nullable: false),
                    AttributeType = table.Column<Guid>(type: "uuid", nullable: false),
                    LogoId = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("a484a778-a4f4-4606-aac5-b47db6705612")),
                    ShopTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shop_attribute_AttributeType",
                        column: x => x.AttributeType,
                        principalTable: "attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_shop_AttributeType",
                table: "shop",
                column: "AttributeType");

            migrationBuilder.CreateIndex(
                name: "IX_shop_LogoId",
                table: "shop",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_shop_ShopTypeId",
                table: "shop",
                column: "ShopTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shop");
        }
    }
}
