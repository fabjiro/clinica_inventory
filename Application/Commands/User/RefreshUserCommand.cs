using Application.Dto.User;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.User;

public record RefreshUserCommand(string refreshToken) : IRequest<Result<AuthUserResDto>>; 