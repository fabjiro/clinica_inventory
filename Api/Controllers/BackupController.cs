
using Application.Helpers;
using Application.Commands.Backup;
using Application.Queries.Backup;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

[ApiController]
[Route("api/[controller]")]
public class BackupController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create()
    {
        var result = await _mediator.Send(new CreateBackupCommand());

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var category = await _mediator.Send(new GetAllBackupQuery());
        return Ok(category);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteBackupCommand(id));

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }

    [HttpPost("restore/{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Restore(Guid id)
    {
        var result = await _mediator.Send(new RestoreBackupCommand(id));

        if (result.IsInvalid())
        {
            var invalidError = ErrorHelper.GetValidationErrors(result.ValidationErrors.ToList());
            return Problem(invalidError, null, 400);
        }

        return Ok(result.Value);
    }
}