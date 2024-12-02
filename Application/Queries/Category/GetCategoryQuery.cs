using Application.Dto.Response.Category;
using MediatR;

namespace Application.Queries.Category;

public record GetCategoryQuery(string UserId) : IRequest<List<CategoryResDto>> { }
