using System.Security.Claims;
using Application.Commands.Inventory;
using Application.Dto.Request.Inventory;
using Application.Helpers;
using Application.Queries.Inventory;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public InventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [Authorize(Policy = "AdminOrSeller")]
    public async Task<IActionResult> Create([FromBody] AddInventoryReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var typeMovement = Guid.Parse(dto.TypeMovement);

            var result = await _mediator.Send(new AddInventoryHistoryCommand(
                typeMovement,
                Guid.Parse(dto.Product),
                dto.Count,
                userId!
            ));

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



    [HttpGet("history")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetHistory()
    {
        try
        {

            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var result = await _mediator.Send(new GetAllInventoryHistoryQuery(
                Guid.Parse(userId!)
            ));

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