using Application.Helpers;
using Application.Queries.Report;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("top-patient-by-consult")]
    [Authorize]
    public async Task<IActionResult> GetReport(
        [FromQuery] int top = 5
    )
    {
        try
        {
            var result = await _mediator.Send(new ReportTopPatientQuery(
                Top: top
            ));
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }
}