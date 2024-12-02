using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Category;

public record CreateCategoryCommand(string UserId, string Name) : IRequest<Result<CategoryEntity>>;
