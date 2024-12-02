using Application.Dto.Response.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;
       

public record AddUserCommand(string UserId, string Name, string Email, string? Password, Guid? RolId, Guid? StatusId, string? Avatar) : IRequest<Result<UserBasicResDto>>;