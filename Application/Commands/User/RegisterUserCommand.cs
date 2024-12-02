using Application.Dto.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;

public record RegisterUserCommand(string fullname, string email, string password) : IRequest<Result<AuthUserResDto>>;
