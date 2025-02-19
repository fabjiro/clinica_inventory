
using Application.Commands.Rol;
using Application.Helpers;
using Application.Queries.Rol;
using Application.Querys.Rol;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
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

    [HttpGet("subrol")]
    public async Task<IActionResult> GetSubRol()
    {
        var categorys = await _mediator.Send(new GetAllSubRolQuery());
        return Ok(categorys.Value);
    }

    [HttpPost("subrol")]
    public async Task<IActionResult> AddSubRol([FromBody] AddSubRolCommad command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("subrol/{id}")]
    public async Task<IActionResult> DeleteSubRol(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteSubRolCommand(id));

            if (result.IsInvalid())
            {
                var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
                return Problem(invalidError, null, 400);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpPut("subrol")]
    public async Task<IActionResult> UpdateSubRol([FromBody] UpdateSubRolCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result.Value);
    }
}