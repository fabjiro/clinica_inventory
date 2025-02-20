using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Page;

public class GetPagePermitSpecifications : Specification<PagePermitsEntity>
{
    public GetPagePermitSpecifications(
        Guid? pageId = null,
        Guid? rolId = null,
        bool include = false
    )
    {

        if (pageId is not null  && rolId is not null )
        {
            Query.Where(x => x.PageId == pageId);
            Query.Where(x => x.SubRolId == rolId);
        }


        if(pageId is not null)
        {
            Query.Where(x => x.PageId == pageId);
        }

        if(rolId is not null)
        {
            Query.Where(x => x.SubRolId == rolId);
        }

        if(include) 
        {
            Query.Include(x => x.Page);
            Query.Include(x => x.SubRol);
        }
    }
}