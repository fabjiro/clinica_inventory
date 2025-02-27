using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetAllUserIncludeSpecification : Specification<UserEntity>
{
    public GetAllUserIncludeSpecification()
    {
        Query.Where(x => !x.IsDeleted);
        Query.Include(x => x.Rol);
        Query.Include(x => x.Avatar);

        Query.OrderByDescending(x => x.CreatedAt);
    }
}