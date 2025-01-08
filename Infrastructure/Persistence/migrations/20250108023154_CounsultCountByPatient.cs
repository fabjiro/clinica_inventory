using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class CounsultCountByPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "consult");

            migrationBuilder.AddColumn<int>(
                name: "ConsultCount",
                table: "patient",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultCount",
                table: "patient");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "consult",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
