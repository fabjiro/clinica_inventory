using Ardalis.Specification;
using Domain.Entities.Shop;

namespace Application.Specifications.Shop;

public class ShopIncludesSpecifications : SingleResultSpecification<ShopEntity>
{
    public ShopIncludesSpecifications(Guid? shopId = null)
    {
        Query.Include(x => x.ShopType);
        Query.Include(x => x.Logo);

        if (shopId is not null)
        {
            Query.Where(x => x.Id == shopId);
        }
    }
}