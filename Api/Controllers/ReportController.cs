using Application.Helpers;
using Application.Queries.Report;
using Ardalis.Result;
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

    [HttpGet("consult-by-date")]
    [Authorize]
    public async Task<IActionResult> ConsultByDate()
    {
        try
        {
            var result = await _mediator.Send(new ReportConsultByDateQuery());
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpGet("count-patient-by-date")]
    [Authorize]
    public async Task<IActionResult> ConsultByDoctor()
    {
        try
        {
            var result = await _mediator.Send(new ReportConsultCountPatientByDateQuery());
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpGet("register-patient")]
    [Authorize]
    public async Task<IActionResult> RegisterPatient(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null
    )
    {
        try
        {
            var result = await _mediator.Send(new ReportRegisterPatientQuery(
                StartDate: startDate,
                EndDate: endDate
            ));

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpGet("register-patient-by-user")]
    [Authorize]
    public async Task<IActionResult> RegisterPatientByUser(
        [FromQuery] string userId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null
    )
    {
        try
        {
            var result = await _mediator.Send(new ReportRegistrationByUser(
                StartDate: startDate,
                EndDate: endDate,
                UserId: userId
            ));

            if(result.IsInvalid())
            {
                var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
                return Problem(invalidError, null, 400);

            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }


    [HttpGet("recent-diagnostics")]
    [Authorize]
    public async Task<IActionResult> RecentDiagnostics(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null
    )
    {
        try
        {
            var result = await _mediator.Send(new ReportDiagnosticsQuery(
                StartDate: startDate,
                EndDate: endDate
            ));
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }
}