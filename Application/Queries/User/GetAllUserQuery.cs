using Application.Dto.Response.User;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.User;

public record GetAllUserQuery(string UserId) : IRequest<Result<List<UserBasicResDto>>> { }