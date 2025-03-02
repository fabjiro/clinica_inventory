using Ardalis.Specification;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Specifications.Consult;

public class GetConsultByRangeDateSpcecifications : Specification<ConsultEntity>
{
    public GetConsultByRangeDateSpcecifications(
        DateTime? startDate = null,
        DateTime? endDate = null,
        string? userId = null,
        bool include = false
    )
    {
        Query.Where(x => x.IsDeleted == false);

        if(startDate != null && endDate != null) {
            Query.Where(x => x.Nextappointment >= startDate && x.Nextappointment <= endDate);
        } else if(startDate != null) {
            Query.Where(x => x.Nextappointment >= startDate);
        } else if(endDate != null) {
            Query.Where(x => x.Nextappointment <= endDate);
        }

        if(userId != null) {
            Query.Where(x => x.CreatedBy == userId);
        }

        if(include) {
            Query.Include(x => x.Patient);
            Query.Include(x => x.UserCreatedBy);
            Query.Include(x => x.UserCreatedBy!.Avatar);
            Query.Include(x => x.UserCreatedBy!.Rol);
            Query.Include(x => x.UserCreatedBy!.Rol!.Rol);
        }
    }
}