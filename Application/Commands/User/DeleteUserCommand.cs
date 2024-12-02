using Application.Dto.Response.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;

public record DeleteUserCommand(string UserId, Guid Id) : IRequest<Result<UserBasicResDto>>;  