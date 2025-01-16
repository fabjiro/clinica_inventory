using Ardalis.Specification;

namespace Application.Specifications.Consult;

public class GetReportConsultByDateSpecifications : Specification<ConsultEntity>
{
    public GetReportConsultByDateSpecifications(
        DateTime startDate,
        DateTime endDate
    )
    {
        Query.Where(x => x.IsDeleted == false);
        Query.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
    }
}