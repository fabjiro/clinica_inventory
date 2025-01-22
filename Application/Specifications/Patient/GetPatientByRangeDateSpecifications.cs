using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient
{
    public class GetPatientByRangeDateSpecifications : Specification<PatientEntity>
    {
        public GetPatientByRangeDateSpecifications(
            DateTime? startDate = null, 
            DateTime? endDate = null
        )
        {
            Query.Where(x => x.IsDeleted == false);

            if(startDate != null && endDate != null) {
                Query.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
            } else if(startDate != null) {
                Query.Where(x => x.CreatedAt >= startDate);
            } else if(endDate != null) {
                Query.Where(x => x.CreatedAt <= endDate);
            }
        }
    }
}