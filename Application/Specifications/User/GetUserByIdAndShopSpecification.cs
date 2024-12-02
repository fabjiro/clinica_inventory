
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetUserByIdAndShopSpecifications : SingleResultSpecification<UserEntity>
{
    public GetUserByIdAndShopSpecifications(Guid id, Guid shopId)
    {
        Query.Where(x => x.Id == id);
        Query.Where(x => x.ShopId == shopId);
    }
}