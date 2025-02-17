using Ardalis.Specification;
using Domain.Entities;

namespace Application.Handlers.User;

public class GetUserByRolSpecification : Specification<UserEntity> 
{
    public GetUserByRolSpecification(Guid rol) {
        Query.Include(x => x.Rol);
        Query.Include(x => x.Avatar);
        Query.Where(x => x.SubRolId == rol);
    }
}