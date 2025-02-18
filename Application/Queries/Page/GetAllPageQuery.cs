using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Page;

public record GetAllPageQuery : IRequest<Result<List<PageEntity>>> {}