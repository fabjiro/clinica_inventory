using Application.Dto.Response.User;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.User;

public record GetMeUserQuery(Guid Id) : IRequest<Result<UserBasicResDto>>;