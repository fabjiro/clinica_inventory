using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Product;

public class GetProductByIdAndShopSpecification : SingleResultSpecification<ProductEntity>
{
    public GetProductByIdAndShopSpecification(Guid id, Guid shopId)
    {
        Query.Where(x => x.Id == id);
        Query.Where(x => x.ShopId == shopId);
    }
}