
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.User;

public class GetUserByEmailSpecifications : SingleResultSpecification<UserEntity>
{
    public GetUserByEmailSpecifications(string email)
    {
        Query.Where(x => x.Email == email);
    }
}