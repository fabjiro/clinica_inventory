using Application.Helpers;
using Application.Queries.CivilStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CivilStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public CivilStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categorys = await _mediator.Send(new GetAllCivilStatusQuery());
            return Ok(categorys.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

}