using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ProductAttributesValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttributeValueId",
                table: "product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_AttributeValueId",
                table: "product",
                column: "AttributeValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_attributes_values_AttributeValueId",
                table: "product",
                column: "AttributeValueId",
                principalTable: "attributes_values",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_attributes_values_AttributeValueId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_AttributeValueId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "AttributeValueId",
                table: "product");
        }
    }
}
