using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class userAndCivilstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "user",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "user",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CivilStatusId",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identification",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeSex",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "civil_status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_civil_status", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_CivilStatusId",
                table: "user",
                column: "CivilStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_civil_status_CivilStatusId",
                table: "user",
                column: "CivilStatusId",
                principalTable: "civil_status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_civil_status_CivilStatusId",
                table: "user");

            migrationBuilder.DropTable(
                name: "civil_status");

            migrationBuilder.DropIndex(
                name: "IX_user_CivilStatusId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "user");

            migrationBuilder.DropColumn(
                name: "CivilStatusId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Identification",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "user");

            migrationBuilder.DropColumn(
                name: "TypeSex",
                table: "user");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
