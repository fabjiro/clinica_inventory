using System.Security.Claims;
using Application.Commands.Attributes;
using Application.Dto.Request.Attribute;
using Application.Helpers;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AttributeValueController : ControllerBase
{

    private readonly IMediator _mediator;

    public AttributeValueController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAttributeValueReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new CreateAttributeValueCommand(userId!, Guid.Parse(dto.Attribute), dto.Value);
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
}