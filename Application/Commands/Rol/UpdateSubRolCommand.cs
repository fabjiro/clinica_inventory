using Ardalis.Result;
using MediatR;

namespace Application.Commands.Rol;

public record UpdateSubRolCommand(
    Guid Id,
    string Name,
    Guid? RolId = null
) : IRequest<Result<Guid>> {}