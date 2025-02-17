using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class UserSubRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_rol_RolId",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "RolId",
                table: "user",
                newName: "SubRolId");

            migrationBuilder.RenameIndex(
                name: "IX_user_RolId",
                table: "user",
                newName: "IX_user_SubRolId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_sub_rol_SubRolId",
                table: "user",
                column: "SubRolId",
                principalTable: "sub_rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_sub_rol_SubRolId",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "SubRolId",
                table: "user",
                newName: "RolId");

            migrationBuilder.RenameIndex(
                name: "IX_user_SubRolId",
                table: "user",
                newName: "IX_user_RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_rol_RolId",
                table: "user",
                column: "RolId",
                principalTable: "rol",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
