using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Rol
{
    public class SubRolByIdIncludesSpecifications : Specification<SubRolEntity>
    {
        public SubRolByIdIncludesSpecifications()
        {
            Query.Include(x => x.Rol);
            Query.OrderByDescending(x => x.Name);
        }
    }
}