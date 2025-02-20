using Ardalis.Specification;

namespace Application.Specifications.Consult;

public class GetConsultIncludeSpecifications : Specification<ConsultEntity>
{
    public GetConsultIncludeSpecifications(
        Guid? PatientId = null,
        DateTime? StartDate = null,
        DateTime? EndDate = null

    )
    {
        if(PatientId is not null)
        {
            Query.Where(x => x.PatientId == PatientId);
        }

        if(StartDate is not null && EndDate is not null) {
            Query.Where(x => x.CreatedAt >= StartDate && x.CreatedAt <= EndDate);
        } else if(StartDate is not null) {
            Query.Where(x => x.CreatedAt >= StartDate);
        } else if(EndDate is not null) {
            Query.Where(x => x.CreatedAt <= EndDate);
        }


        Query.Include(x => x.Patient);
        Query.Include(x => x.ComplementaryTest);
        Query.Include(x => x.Image);
        
            Query.Include(x => x.UserCreatedBy);
            Query.Include(x => x.UserCreatedBy!.Avatar);
            Query.Include(x => x.UserCreatedBy!.Rol);

        Query.Where(x => x.IsDeleted == false);

        Query.OrderByDescending(x => x.CreatedAt);
    }
}