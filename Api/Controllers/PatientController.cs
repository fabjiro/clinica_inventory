

using Application.Helpers;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categorys = await _mediator.Send(new GetAllPatientQuery());
            return Ok(categorys.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }
}