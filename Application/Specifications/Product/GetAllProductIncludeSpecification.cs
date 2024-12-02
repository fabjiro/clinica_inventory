using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Product;

public class GetAllProductIncludeSpecification : Specification<ProductEntity>
{
    public GetAllProductIncludeSpecification(Guid? shopId = null)
    {
        Query.Include(x => x.Category);
        Query.Include(x => x.Image);
        Query.Include(x => x.AttributeValue);
        Query.Where(emp => emp.IsDeleted == false);

        if (shopId is not null)
        {
            Query.Where(x => x.ShopId == shopId);
        }

    }
}