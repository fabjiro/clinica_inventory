
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetUserByEmailAndShopSpecifications : SingleResultSpecification<UserEntity>
{
    public GetUserByEmailAndShopSpecifications(string email, Guid shopId)
    {
        Query.Where(x => x.Email == email);
        Query.Where(x => x.ShopId == shopId);
    }
}