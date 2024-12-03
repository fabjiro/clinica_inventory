using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetUserByIdentification : SingleResultSpecification<UserEntity>
{
    public GetUserByIdentification(string identification)
    {
        Query.Where(x => x.Identification == identification);
    }
}