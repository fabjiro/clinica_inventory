using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetAllUserByShopIncludeSpecification : Specification<UserEntity>
{
    public GetAllUserByShopIncludeSpecification(Guid shopId)
    {
        Query.Where(emp => emp.IsDeleted == false);
        Query.Include(x => x.Rol);
        Query.Include(x => x.Avatar);
    }
}