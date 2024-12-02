using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Category;

public class GetCategoryByIdAndShopSpecification : SingleResultSpecification<CategoryEntity>
{
    public GetCategoryByIdAndShopSpecification(Guid id, Guid shopId)
    {
        Query.Where(x => x.Id == id);
        Query.Where(x => x.ShopId == shopId);
    }
}