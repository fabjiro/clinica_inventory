

using System.Security.Claims;
using Application.Commands.Patient;
using Application.Dto.Request.Patient;
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
                Birthday: DateTime.SpecifyKind(dto.Birthday!.Value, DateTimeKind.Utc),
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

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new DeletePatientCommand(userId! ,id);
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

    [HttpPut]
    [Authorize(Policy = "AdminOrReception")]
    public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientReqDto dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new UpdatePatientCommand(
                userId!,
                dto.Id,
                Name: dto.Name,
                Identification: dto.Identification,
                Phone: dto.Phone,
                Address: dto.Address,
                ContactPerson: dto.ContactPerson,
                ContactPhone: dto.ContactPhone,
                Age: dto.Age,
                BirthDate: dto.Birthday != null ? DateTime.SpecifyKind(dto.Birthday.Value, DateTimeKind.Utc) : null,
                TypeSex: dto.TypeSex is not null ? Guid.Parse(dto.TypeSex) : null,
                CivilStatus: dto.CivilStatus is not null ? Guid.Parse(dto.CivilStatus) : null,
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