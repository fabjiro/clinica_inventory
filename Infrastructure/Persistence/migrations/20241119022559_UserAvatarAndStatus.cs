using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class UserAvatarAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AvatarId",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("796d8cb3-df73-44d4-9932-32b9988f152c"));

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "user",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("9710ae5e-74ba-4cdf-9c05-f408a0b4c817"));

            migrationBuilder.CreateIndex(
                name: "IX_user_AvatarId",
                table: "user",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_user_StatusId",
                table: "user",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_image_AvatarId",
                table: "user",
                column: "AvatarId",
                principalTable: "image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_status_StatusId",
                table: "user",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_image_AvatarId",
                table: "user");

            migrationBuilder.DropForeignKey(
                name: "FK_user_status_StatusId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_AvatarId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_StatusId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "user");
        }
    }
}
