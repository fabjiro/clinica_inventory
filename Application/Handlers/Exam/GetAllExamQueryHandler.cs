using Application.Dto.Response.Exam;
using Application.Queries.Exam;
using Application.Specifications.Exam;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Exam
{
    public class GetAllExamQueryHandler : IRequestHandler<GetAllExamQuery, List<ExamDto>>
    {
        private readonly IAsyncRepository<ExamEntity> _examRepository;
        private readonly IMapper _mapper;

        public GetAllExamQueryHandler(IAsyncRepository<ExamEntity> examRepository, IMapper mapper)
        {
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<List<ExamDto>> Handle(GetAllExamQuery request, CancellationToken cancellationToken)
        {
            var exams = await _examRepository.ListAsync(new IncludeExamSpecification(), cancellationToken);
            return _mapper.Map<List<ExamDto>>(exams);
        }
    }
}