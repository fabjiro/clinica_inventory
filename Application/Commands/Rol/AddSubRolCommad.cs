using Ardalis.Result;
using MediatR;

namespace Application.Commands.Rol;

public record AddSubRolCommad(
    string Name,
    Guid RolId
) : IRequest<Result<Guid>>;