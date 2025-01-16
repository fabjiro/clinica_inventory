using Application.Dto.Response.Report;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Patient;

public class GetTopPatientSpecifications : Specification<PatientEntity, ReportTopPatientDto>
{
    public GetTopPatientSpecifications(
        int take = 5
    )
    {
        Query.Where(x => x.IsDeleted == false);
        
        Query.OrderByDescending(x => x.ConsultCount);

        Query.Take(take);
    
        Query.Select(x => new ReportTopPatientDto
        {
            Name = x.Name,
            Total = x.ConsultCount
        });
    }
}