using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.migrations
{
    /// <inheritdoc />
    public partial class ConsultEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Motive = table.Column<string>(type: "text", nullable: false),
                    Clinicalhistory = table.Column<string>(type: "text", nullable: true),
                    BilogicalEvaluation = table.Column<string>(type: "text", nullable: true),
                    PsychologicalEvaluation = table.Column<string>(type: "text", nullable: true),
                    SocialEvaluation = table.Column<string>(type: "text", nullable: true),
                    FunctionalEvaluation = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    Size = table.Column<decimal>(type: "numeric", nullable: false),
                    Pulse = table.Column<decimal>(type: "numeric", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "numeric", nullable: true),
                    SystolicPressure = table.Column<decimal>(type: "numeric", nullable: true),
                    DiastolicPressure = table.Column<decimal>(type: "numeric", nullable: true),
                    AntecedentPersonal = table.Column<string>(type: "text", nullable: false),
                    AntecedentFamily = table.Column<string>(type: "text", nullable: true),
                    ExamComplementaryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Diagnosis = table.Column<string>(type: "text", nullable: false),
                    ImageExamId = table.Column<Guid>(type: "uuid", nullable: true),
                    Recipe = table.Column<string>(type: "text", nullable: false),
                    Nextappointment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_consult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_consult_exam_ExamComplementaryId",
                        column: x => x.ExamComplementaryId,
                        principalTable: "exam",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_consult_image_ImageExamId",
                        column: x => x.ImageExamId,
                        principalTable: "image",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_consult_patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_consult_ExamComplementaryId",
                table: "consult",
                column: "ExamComplementaryId");

            migrationBuilder.CreateIndex(
                name: "IX_consult_ImageExamId",
                table: "consult",
                column: "ImageExamId");

            migrationBuilder.CreateIndex(
                name: "IX_consult_PatientId",
                table: "consult",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consult");
        }
    }
}
