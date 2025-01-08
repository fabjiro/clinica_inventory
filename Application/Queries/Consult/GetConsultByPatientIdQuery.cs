using Application.Dto.Response.Consult;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Consult;

public record GetConsultByPatientIdQuery(Guid PatientId) : IRequest<Result<List<ConsultDtoRes>>>{}