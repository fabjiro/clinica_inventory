using System.Security.Claims;
using Application.Commands.Attributes;
using Application.Dto.Request.Attribute;
using Application.Helpers;
using Application.Queries.Attributes;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AttributeController : ControllerBase
{
    private readonly IMediator _mediator;

    public AttributeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var categorys = await _mediator.Send(new GetAllAttributesQuery());
        return Ok(categorys.Value);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAttributeReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new CreateAttributeCommand(userId!, dto.Name);

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