using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient;

public class InclusePatientSpecification : Specification<PatientEntity>
{
    public InclusePatientSpecification()
    {
        Query.Include(x => x.Avatar);
        Query.Include(x => x.CivilStatus);
        Query.Include(x => x.Rol);
    }
}