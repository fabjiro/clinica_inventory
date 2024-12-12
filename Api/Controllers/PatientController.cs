

using System.Security.Claims;
using Application.Commands.Patient;
using Application.Dto.Request.User;
using Application.Helpers;
using Application.Queries.Patient;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categorys = await _mediator.Send(new GetAllPatientQuery());
            return Ok(categorys.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpPost]
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

}