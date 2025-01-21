using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient
{
    public class GetPatientByRangeDateSpecifications : Specification<PatientEntity>
    {
        public GetPatientByRangeDateSpecifications(DateTime startDate, DateTime endDate)
        {
            Query.Where(x => x.IsDeleted == false);
            Query.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
        }
    }
}