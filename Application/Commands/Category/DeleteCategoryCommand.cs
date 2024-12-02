using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public record DeleteCategoryCommand(string UserId, Guid Id) : IRequest<Result<CategoryEntity>>;