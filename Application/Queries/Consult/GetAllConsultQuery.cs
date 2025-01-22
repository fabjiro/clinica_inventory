using Application.Dto.Response.Consult;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Consult;

public record GetAllConsultQuery(
    DateTime? StartDate = null,
    DateTime? EndDate = null
) : IRequest<Result<List<ConsultDtoRes>>>{};