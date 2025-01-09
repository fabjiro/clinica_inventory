using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ConsultCreateByMod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consult_user_UserCreatedById",
                table: "consult");

            migrationBuilder.RenameColumn(
                name: "UserCreatedById",
                table: "consult",
                newName: "CreatedByGuid");

            migrationBuilder.RenameIndex(
                name: "IX_consult_UserCreatedById",
                table: "consult",
                newName: "IX_consult_CreatedByGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_consult_user_CreatedByGuid",
                table: "consult",
                column: "CreatedByGuid",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consult_user_CreatedByGuid",
                table: "consult");

            migrationBuilder.RenameColumn(
                name: "CreatedByGuid",
                table: "consult",
                newName: "UserCreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_consult_CreatedByGuid",
                table: "consult",
                newName: "IX_consult_UserCreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_consult_user_UserCreatedById",
                table: "consult",
                column: "UserCreatedById",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
