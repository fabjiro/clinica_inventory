using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Specifications.Consult;

public class GetConsultByRangeDateSpcecificationss : Specification<ConsultEntity>
{
    public GetConsultByRangeDateSpcecificationss(
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? userId = null,
        bool include = false
    )
    {
        Query.Where(x => x.IsDeleted == false);

        if (startDate != null && endDate != null)
        {
            Query.Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate);
        }
        else if (startDate != null)
        {
            Query.Where(x => x.CreatedAt >= startDate);
        }
        else if (endDate != null)
        {
            Query.Where(x => x.CreatedAt <= endDate);
        }

        if (userId != null)
        {
            Query.Where(x => x.CreatedBy == userId);
        }

        if (include)
        {
            Query.Include(x => x.Patient);
            Query.Include(x => x.UserCreatedBy);
            Query.Include(x => x.UserCreatedBy!.Avatar);
            Query.Include(x => x.UserCreatedBy!.Rol);
            Query.Include(x => x.UserCreatedBy!.Rol!.Rol);
        }
    }
}