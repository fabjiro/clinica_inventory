using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Category;

public class GetAllCategoryByShopIdSpecifications : Specification<CategoryEntity>
{

    public GetAllCategoryByShopIdSpecifications(Guid shopId)
    {
        Query.Where(emp => emp.IsDeleted == false);
        Query.Where(x => x.ShopId == shopId);
        Query.Include(x => x.Products);
    }
}