using System.Security.Claims;
using Application.Helpers;
using Application.Commands.User;
using Application.Dto.Request.Auth;
using Application.Queries.User;
using Ardalis.Result;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;

    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = new GetMeUserQuery(Guid.Parse(userId!));
        var result = await _mediator.Send(command);
        return Ok(result.Value);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthRegisterUserReqDto bodyData)
    {

        var result = await _mediator.Send(new RegisterUserCommand(
            bodyData.Name,
            bodyData.Email,
            bodyData.Password
        ));

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginUserReqDto bodyData)
    {
        var command = new LoginUserCommand(bodyData.Email, bodyData.Password);
        var result = await _mediator.Send(command);

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] AuthRefreshUserReqDto bodyData)
    {
        var command = new RefreshUserCommand(bodyData.RefreshToken);
        var result = await _mediator.Send(command);

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

}