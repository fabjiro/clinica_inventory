using Application.Helpers;
using Application.Dto.Request.User;
using Application.Queries.User;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Commands.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var category = await _mediator.Send(new GetAllUserQuery(userId!));
        return Ok(category.Value);
    }

    [HttpPut]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update([FromBody] UserUpdateReqDto dto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var commands = new UpdateUserCommand(userId!, Guid.Parse(dto.Id), dto.Name, dto.Password, dto.Rol, dto.Avatar);
        var result = await _mediator.Send(commands);
        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);

        }
        return Ok(result.Value);
    }

    [HttpPost("add")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Add([FromBody] UserAddReqDto dto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var commands = new AddUserCommand(userId!, dto.Name, dto.Email, dto.Password, (dto.Rol != null ? Guid.Parse(dto.Rol) : null), dto.Avatar);
        var result = await _mediator.Send(commands);

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await _mediator.Send(new DeleteUserCommand(userId!, id));

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }
        return Ok(result.Value);
    }

}