using Application.Dto.Response.Group;

namespace Application.Dto.Response.Exam
{
    public class ExamDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public GroupDto? Group { get; set; }
    }
}