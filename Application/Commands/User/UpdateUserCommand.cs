using Application.Dto.Response.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;

// convert params to record

public record UpdateUserCommand(string UserId, Guid id, string? name, string? password, Guid? rolId, string? Avatar) : IRequest<Result<UserBasicResDto>>;