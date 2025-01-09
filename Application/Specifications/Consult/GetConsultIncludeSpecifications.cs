using Ardalis.Specification;

namespace Application.Specifications.Consult;

public class GetConsultIncludeSpecifications : Specification<ConsultEntity>
{
    public GetConsultIncludeSpecifications(
        Guid? PatientId = null
    )
    {
        if(PatientId is not null)
        {
            Query.Where(x => x.PatientId == PatientId);
        }


        Query.Include(x => x.Patient);
        Query.Include(x => x.ComplementaryTest);
        Query.Include(x => x.Image);
        
        Query.Include(x => x.UserCreatedBy);

        Query.Where(x => x.IsDeleted == false);
    }
}