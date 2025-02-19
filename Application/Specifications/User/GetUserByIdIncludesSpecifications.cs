using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetUserByIdIncludesSpecifications : SingleResultSpecification<UserEntity>
{
    public GetUserByIdIncludesSpecifications(Guid id)
    {
        Query.Where(x => x.Id == id);
        Query.Include(x => x.Rol);
        Query.Include(x => x.Rol!.Rol);
        Query.Include(x => x.Avatar);
    }
}