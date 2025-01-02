using Application.Dto.Response.Consult;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Consult;

public record UpdateConsultCommand(
    Guid Id,
    string UserId,
    string? Motive = null,
    string? AntecedentPersonal = null,
    string? Diagnostic = null,
    string? Recipe = null,
    decimal? Weight = null,
    decimal? Size = null,
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
    DateTime? Nextappointment = null,
    Guid? PatientId = null,
    Guid? ExamComplementaryId = null
) : IRequest<Result<ConsultDtoRes>>
{ };