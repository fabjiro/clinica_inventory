using System.Security.Claims;
using Application.Commands.Exam;
using Application.Dto.Request.Exam;
using Application.Dto.Response.Exam;
using Application.Helpers;
using Application.Queries.Exam;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ExamDto>>> GetAll()
    {
        var exams = await _mediator.Send(new GetAllExamQuery());
        return Ok(exams);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ExamDto>> Create([FromBody] CreateExamDtoReq dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new CreateExamCommand(userId!, dto.Name, Guid.Parse(dto.Group));
            var exam = await _mediator.Send(command);

            if (exam.IsInvalid())
            {
                var invalidError = ErrorHelper.GetValidationErrors(exam.ValidationErrors.ToList());
                return Problem(invalidError, null, 400);
            }

            return Ok(exam.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<ExamDto>> Update([FromBody] UpdateExamDtoReq dto)
    {
        try
        {
            var command = new UpdateExamCommand(Guid.Parse(dto.Id), Name: dto.Name, Group: string.IsNullOrWhiteSpace(dto.Group) ? null : Guid.Parse(dto.Group));
            var exam = await _mediator.Send(command);

            if (exam.IsInvalid())
            {
                var invalidError = ErrorHelper.GetValidationErrors(exam.ValidationErrors.ToList());
                return Problem(invalidError, null, 400);
            }

            return Ok(exam.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Guid>> Delete(Guid id)
    {
        try
        {
            var command = new DeleteExamCommand(id);
            var exam = await _mediator.Send(command);

            if (exam.IsInvalid())
            {
                var invalidError = ErrorHelper.GetValidationErrors(exam.ValidationErrors.ToList());
                return Problem(invalidError, null, 400);
            }

            return Ok(exam.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }
}