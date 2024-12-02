using System.Security.Claims;
using Application.Commands.Shop;
using Application.Dto.Request.Shop;
using Application.Helpers;
using Application.Queries.Shop;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ShopController : ControllerBase
{
    public readonly IMediator _mediator;

    public ShopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateShopReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var command = new CreateShopCommand(
                userId!,
                dto.Name,
                dto.MinStockProducts,
                Guid.Parse(dto.ShopType),
                Guid.Parse(dto.Attribute),
                dto.Logo
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


    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = new GetMeShopQuery(userId!);
        var result = await _mediator.Send(command);

        if(result.IsInvalid()) {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }
}