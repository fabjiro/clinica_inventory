using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ProductBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "product",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("bf4b44d4-95dc-4622-810f-0cad0895cf48"),
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "product",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "product",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "product",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "product",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "product");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "product");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "product");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "product");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "product");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "product");


            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "product",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<Guid>(
                name: "ImageId",
                table: "product",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("bf4b44d4-95dc-4622-810f-0cad0895cf48"));
        }
    }
}
