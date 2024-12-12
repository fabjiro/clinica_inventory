using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class userAndPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_civil_status_CivilStatusId",
                table: "user");

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
                name: "Password",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "patient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Identification = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    ContactPerson = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TypeSex = table.Column<Guid>(type: "uuid", nullable: true),
                    CivilStatusId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uuid", nullable: false),
                    RolId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_patient_civil_status_CivilStatusId",
                        column: x => x.CivilStatusId,
                        principalTable: "civil_status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_patient_image_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "image",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_rol_RolId",
                        column: x => x.RolId,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_patient_AvatarId",
                table: "patient",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_patient_CivilStatusId",
                table: "patient",
                column: "CivilStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_patient_RolId",
                table: "patient",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patient");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
