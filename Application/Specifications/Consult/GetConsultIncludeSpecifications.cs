using Ardalis.Specification;

namespace Application.Specifications.Consult;

public class GetConsultIncludeSpecifications : Specification<ConsultEntity>
{
    public GetConsultIncludeSpecifications()
    {
        Query.Include(x => x.Patient);
        Query.Include(x => x.ComplementaryTest);
        Query.Include(x => x.Image);
    }
}