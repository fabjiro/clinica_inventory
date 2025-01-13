using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient;

public class InclusePatientSpecification : Specification<PatientEntity>
{
    public InclusePatientSpecification()
    {   
        Query.Where(x => x.IsDeleted == false);

        Query.Include(x => x.Avatar);
        Query.Include(x => x.CivilStatus);
        Query.Include(x => x.Rol);

        Query.OrderBy(x => x.Name);
    }
}