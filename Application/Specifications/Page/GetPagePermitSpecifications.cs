using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Page;

public class GetPagePermitSpecifications : Specification<PagePermitsEntity>
{
    public GetPagePermitSpecifications(Guid pageId, Guid rolId)
    {
        Query.Where(x => x.PageId == pageId);
        Query.Where(x => x.SubRolId == rolId);
    }
}