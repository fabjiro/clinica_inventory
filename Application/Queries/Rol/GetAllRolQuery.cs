using Application.Dto.Request.Rol;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Rol;

public class GetAllRolQuery : IRequest<Result<List<RolResDto>>> {}