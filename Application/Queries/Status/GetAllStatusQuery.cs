using Application.Dto.Response.Status;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Status;

public class GetAllStatusQuery : IRequest<Result<List<StatusResDto>>> {}