using Application.Dto.Response.Patient;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Patient;

public record UpdatePatientCommand(
    string UserId, 
    Guid Id,
    int? Age = null,
    string? Name = null,
    string? Identification = null,
    string? Phone = null,
    string? Address = null,
    string? ContactPerson = null,
    string? ContactPhone = null,
    string? Avatar = null,
    DateTime? BirthDate = null,
    Guid? Rol = null,
    Guid? CivilStatus = null,
    Guid? TypeSex = null
) : IRequest<Result<PatientResDto>> {};