using Application.Dto.Response.Consult;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Consult;

public record GetAllConsultQuery : IRequest<Result<List<ConsultDtoRes>>>{};