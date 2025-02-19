using Ardalis.Result;
using MediatR;

namespace Application.Commands.Page;

public record TogglePagePermitCommand(Guid PageId, Guid RolId) : IRequest<Result<bool>> {}