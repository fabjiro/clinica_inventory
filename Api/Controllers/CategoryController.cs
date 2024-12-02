using Application.Helpers;
using System.Security.Claims;
using Application.Commands.Category;
using Application.Dto.Request.Category;
using Application.Queries.Category;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value; 
        var categorys = await _mediator.Send(new GetCategoryQuery(userId!));
        return Ok(categorys);
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CategoryRequestDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new CreateCategoryCommand(userId!, dto.Name);

            var result = await _mediator.Send(command);

            if (result.IsInvalid())
            {
                if (result.IsInvalid())
                {
                    var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
                    return Problem(invalidError, null, 400);

                }
            }

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpPut]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryReqDto dto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        var command = new UpdateCategoryCommand(userId!, Guid.Parse(dto.Id), dto.Name);
        var result = await _mediator.Send(command);

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

        var result = await _mediator.Send(new DeleteCategoryCommand(userId!, id));

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }
}