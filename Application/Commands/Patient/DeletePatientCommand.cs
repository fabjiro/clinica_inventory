using Application.Dto.Response.Patient;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Patient;

public record DeletePatientCommand(string UserId, Guid Id) : IRequest<Result<PatientResDto>>;