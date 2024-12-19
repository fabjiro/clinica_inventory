using Application.Commands.Exam;
using Application.Dto.Response.Exam;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Exam;

public class UpdateExamCommandHandler : IRequestHandler<UpdateExamCommand, Result<ExamDto>>
{
    private readonly IAsyncRepository<ExamEntity> _examRepository;
    private readonly IAsyncRepository<GroupEntity> _groupRepository;
    private readonly IMapper _mapper;

    public UpdateExamCommandHandler(IAsyncRepository<ExamEntity> examRepository, IAsyncRepository<GroupEntity> groupRepository, IMapper mapper)
    {
        _examRepository = examRepository;
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<Result<ExamDto>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var exam = await _examRepository.GetByIdAsync(request.Id, cancellationToken);
            if (exam == null)
            {
                return Result.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Examn not found",}
            });
            }

            if (request.Group.HasValue)
            {
                var group = await _groupRepository.GetByIdAsync(request.Group.Value, cancellationToken);
                if (group == null)
                {
                    return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Group not found",}
                });
                }
                exam.GroupId = group.Id;
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                exam.Name = request.Name;
            }

            await _examRepository.UpdateAsync(exam, cancellationToken);

            return Result<ExamDto>.Success(_mapper.Map<ExamDto>(exam));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}