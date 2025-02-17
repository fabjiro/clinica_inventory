using Ardalis.Result;
using MediatR;

namespace Application.Commands.Rol;

public record DeleteSubRolCommand(Guid Id) : IRequest<Result<Guid>>;