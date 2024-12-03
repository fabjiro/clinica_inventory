using Application.Dto.Response.CivilStatus;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.CivilStatus;

public record GetAllCivilStatusQuery() : IRequest<Result<List<CivilStatusResDto>>> {};