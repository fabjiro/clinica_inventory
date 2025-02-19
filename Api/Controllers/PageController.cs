

using Application.Commands.Page;
using Application.Queries.Page;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PageController : ControllerBase
{
    private readonly IMediator _mediator;

    public PageController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorys = await _mediator.Send(new GetAllPageQuery());
        return Ok(categorys.Value);
    }
    
    [HttpPost("toggle-page-permit")]
    public async Task<IActionResult> TogglePageWithRol(TogglePagePermitCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result.Value);
    }
}