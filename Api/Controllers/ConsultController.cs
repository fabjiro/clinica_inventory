using System.Security.Claims;
using Application.Commands.Consult;
using Application.Dto.Request.Consult;
using Application.Helpers;
using Application.Queries.Consult;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConsultController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsultController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateConsultDtoReq dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new AddConsultCommand(
                userId!,
                Guid.Parse(dto.Patient),
                dto.Motive,
                dto.AntecedentPerson,
                dto.Diagnostic,
                dto.Recipe,
                DateTime.SpecifyKind(dto.Nextappointment!.Value, DateTimeKind.Utc),
                dto.Weight,
                dto.Size,
                AntecedentFamily: dto.AntecedentFamily,
                Clinicalhistory: dto.Clinicalhistory,
                BilogicalEvaluation: dto.BilogicalEvaluation,
                PsychologicalEvaluation: dto.PsychologicalEvaluation,
                SocialEvaluation: dto.SocialEvaluation,
                FunctionalEvaluation: dto.FunctionalEvaluation,
                Pulse: dto.Pulse,
                OxygenSaturation: dto.OxygenSaturation,
                SystolicPressure: dto.SystolicPressure,
                DiastolicPressure: dto.DiastolicPressure,
                ExamComplementaryId: dto.ExamComplementary != null ? Guid.Parse(dto.ExamComplementary) : null,
                ImageExam: dto.ImageExam
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

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get(
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null
    )
    {
        try
        {
            var result = await _mediator.Send(new GetAllConsultQuery(
                StartDate: startDate,
                EndDate: endDate
            ));
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpGet("{patientId}")]
    [Authorize]
    public async Task<IActionResult> GetByPatient(Guid patientId)
    {
        try
        {
            var result = await _mediator.Send(new GetConsultByPatientIdQuery(
                patientId
            ));

            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var result = await _mediator.Send(new DeleteConsultCommand(
                userId!,
                id
            ));

            if(result.IsInvalid())
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
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateConsultDtoReq dto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var command = new UpdateConsultCommand(
                Guid.Parse(dto.Id),
                UserId: userId!,
                Motive: dto.Motive,
                AntecedentPersonal:dto.AntecedentPerson,
                 Diagnostic: dto.Diagnostic,
                 Recipe: dto.Recipe,
                 Weight: dto.Weight,
                Size: dto.Size,
                AntecedentFamily: dto.AntecedentFamily,
                Clinicalhistory: dto.Clinicalhistory,
                BilogicalEvaluation: dto.BilogicalEvaluation,
                PsychologicalEvaluation: dto.PsychologicalEvaluation,
                SocialEvaluation: dto.SocialEvaluation,
                FunctionalEvaluation: dto.FunctionalEvaluation,
                Pulse: dto.Pulse,
                OxygenSaturation: dto.OxygenSaturation,
                SystolicPressure: dto.SystolicPressure,
                DiastolicPressure: dto.DiastolicPressure,
                ExamComplementaryId: dto.ExamComplementary != null ? Guid.Parse(dto.ExamComplementary) : null,
                ImageExam: dto.ImageExam,
                Nextappointment: dto.Nextappointment != null ?  DateTime.SpecifyKind(dto.Nextappointment.Value, DateTimeKind.Utc) : null
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