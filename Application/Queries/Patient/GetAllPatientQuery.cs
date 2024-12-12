using Application.Dto.Response.Patient;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Patient;

public record GetAllPatientQuery() : IRequest<Result<List<PatientResDto>>>;