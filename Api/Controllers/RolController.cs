
using Application.Queries.Rol;
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
}