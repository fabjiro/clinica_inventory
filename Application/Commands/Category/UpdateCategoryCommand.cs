using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Dto.Request.Category;

public record UpdateCategoryCommand(string UserId, Guid Id, string Name) : IRequest<Result<CategoryEntity>>;