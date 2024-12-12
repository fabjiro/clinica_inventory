using Application.Dto.Response.Patient;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Patient;

public record AddPatientCommand(
    string UserId,
    string Name,
    string Identification,
    string Phone,
    string? Address,
    string ContactPerson,
    string ContactPhone,
    int Age,
    DateTime Birthday,
    Guid TypeSex,
    Guid CivilStatus,
    string? Avatar = null
) : IRequest<Result<PatientResDto>>;