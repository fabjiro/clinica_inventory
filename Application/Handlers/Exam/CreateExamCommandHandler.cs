using Application.Commands.Exam;
using Application.Dto.Response.Exam;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Exam;

public class CreateExamCommandHandler : IRequestHandler<CreateExamCommand, Result<ExamDto>>
{
    private readonly IAsyncRepository<ExamEntity> _examRepository;
    private readonly IAsyncRepository<GroupEntity> _groupRepository;
    private readonly IMapper _mapper;

    public CreateExamCommandHandler(IAsyncRepository<ExamEntity> examRepository, IAsyncRepository<GroupEntity> groupRepository, IMapper mapper)
    {
        _examRepository = examRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<Result<ExamDto>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.GroupId, cancellationToken);

        if (group is null)
        {
            return Result.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Group not found",}
            });
        }

        var exam = new ExamEntity
        {
            Name = request.Name,
            Group = group
        };

        exam.SetCreationInfo(request.UserId);

        await _examRepository.AddAsync(exam, cancellationToken);

        return Result.Success(_mapper.Map<ExamDto>(exam));
    }
}