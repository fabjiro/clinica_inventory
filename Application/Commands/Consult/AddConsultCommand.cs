using Application.Dto.Response.Consult;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Consult;

public record AddConsultCommand(
    string UserId,
    Guid PatientId,
    string Motive,
    string AntecedentPersonal,
    string Diagnostic,
    string Recipe,
    DateTime Nextappointment,
    decimal Weight,
    decimal Size,
    string? AntecedentFamily = null,
    string? Clinicalhistory = null,
    string? BilogicalEvaluation = null,
    string? PsychologicalEvaluation = null,
    string? SocialEvaluation = null,
    string? FunctionalEvaluation = null,
    decimal? Pulse = null,
    decimal? OxygenSaturation = null,
    decimal? SystolicPressure = null,
    decimal? DiastolicPressure = null,
    string? ImageExam = null,
    Guid? ExamComplementaryId = null
) : IRequest<Result<ConsultDtoRes>>{ };