using Application.Dto.Response.Rol;
using Ardalis.Result;
using MediatR;

namespace Application.Querys.Rol;

public record GetAllSubRolQuery : IRequest<Result<List<SubRolResDto>>>{ }