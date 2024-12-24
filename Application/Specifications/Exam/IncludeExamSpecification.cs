using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Exam;



public class IncludeExamSpecification : Specification<ExamEntity>
{
    public IncludeExamSpecification()
    {
        Query.Include(x => x.Group);
        Query.Where(x => x.IsDeleted == false);
        
        Query.OrderByDescending(x => x.CreatedAt);
    }
}
