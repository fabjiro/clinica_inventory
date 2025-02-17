
using Application.Commands.Rol;
using Application.Queries.Rol;
using Application.Querys.Rol;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RolController : ControllerBase
{
    private readonly IMediator _mediator;
    public RolController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorys = await _mediator.Send(new GetAllRolQuery());
        return Ok(categorys.Value);
    }

    [HttpGet("/subrol")]
    public async Task<IActionResult> GetSubRol()
    {
        var categorys = await _mediator.Send(new GetAllSubRolQuery());
        return Ok(categorys.Value);
    }

    [HttpPost("/subrol")]
    public async Task<IActionResult> AddSubRol([FromBody] AddSubRolCommad command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}