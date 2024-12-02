using Application.Dto.Response.Category;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications;

public class GetGaseosaSpecifications : SingleResultSpecification<CategoryEntity, CategoryResDto>
{

    public GetGaseosaSpecifications(string name)
    {
        Query.Where(x => x.Name == name);
        Query.Select(ce => new CategoryResDto { Id = ce.Id.ToString(), Name = ce.Name });
        // Query.Include(ce => ce.Products);
    }
}