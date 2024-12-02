using Application.Dto.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;

public record LoginUserCommand(string Email, string Password) : IRequest<Result<AuthUserResDto>>;