using Application.Dto.Response.Exam;
using Application.Queries.Exam;
using MediatR;
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
    public async Task<ActionResult<List<ExamDto>>> GetAll()
    {
        var exams = await _mediator.Send(new GetAllExamQuery());
        return Ok(exams);
    }
}