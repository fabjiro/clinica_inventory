
using Application.Helpers;
using Application.Commands.Product;
using Application.Dto.Request.Product;
using Application.Queries.Product;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{

    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var products = await _mediator.Send(new GetAllProductQuery(userId!));
        return Ok(products.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateProductReqDto dto)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var command = new CreateProductCommand(userId!, dto.Name, dto.Price, dto.Stock, Guid.Parse(dto.CategoryId), ImageBase64: dto.Image, Description: dto.Description, Attribute: dto.Attribute != null ? Guid.Parse(dto.Attribute) : null);
        var result = await _mediator.Send(command);

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }


        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await _mediator.Send(new DeleteProductCommand(userId!, id));
        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }
        return Ok(result.Value);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateProductReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new UpdateProductCommand(userId!, Guid.Parse(dto.Id), dto.Name, dto.Description, dto.Price, dto.Stock, (dto.CategoryId != null ? Guid.Parse(dto.CategoryId) : null), dto.Image);
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

    [HttpGet("search/{word}")]
    [Authorize]
    public async Task<IActionResult> Search(string word)
    {
        try
        {
            Console.WriteLine(word);
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _mediator.Send(new SearchProductQuery(userId!, word));

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