using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient;

public class GetPatientByIdentification : SingleResultSpecification<PatientEntity>
{
    public GetPatientByIdentification(string identification)
    {
        Query.Where(x => x.IsDeleted == false);
        Query.Where(x => x.Identification == identification);
    }
}