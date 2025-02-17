using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class PagePermits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "page_permit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubRolId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_page_permit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_page_permit_page_PageId",
                        column: x => x.PageId,
                        principalTable: "page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_page_permit_sub_rol_SubRolId",
                        column: x => x.SubRolId,
                        principalTable: "sub_rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_page_permit_PageId",
                table: "page_permit",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_page_permit_SubRolId",
                table: "page_permit",
                column: "SubRolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "page_permit");
        }
    }
}
