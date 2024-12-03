using Application.Dto.Response.User;
using Application.Dto.User;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.User;

public record GetAllPatientsQuery() : IRequest<Result<List<UserBasicResDto>>>;