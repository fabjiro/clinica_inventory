using Application.Helpers;
using Application.Commands.User;
using Application.Dto.Request.User;
using Application.Queries.User;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("patient")]
    [Authorize(Policy = "AdminOrReception")]
    public async Task<IActionResult> CreatePatient([FromBody] AddPatientReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new AddPatientCommand(
                userId!,
                dto.Name,
                dto.Identification,
                dto.Phone,
                dto.Address,
                dto.ContactPerson,
                dto.ContactPhone,
                dto.Age,
                (DateTime)dto.Birthday!,
                Guid.Parse(dto.TypeSex),
                Guid.Parse(dto.CivilStatus),
                Avatar: dto.Avatar
            );
            var result = await _mediator.Send(command);
            if (result.IsInvalid())
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
        var commands = new UpdateUserCommand(userId!, Guid.Parse(dto.Id), dto.Name, dto.Password, dto.Rol, dto.Status, dto.Avatar);
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
        var commands = new AddUserCommand(userId!, dto.Name, dto.Email, dto.Password, (dto.Rol != null ? Guid.Parse(dto.Rol) : null), (dto.Status != null ? Guid.Parse(dto.Status) : null), dto.Avatar);
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