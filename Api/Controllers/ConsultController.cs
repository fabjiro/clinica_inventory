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
                DateTime.Parse(dto.Nextappointment).ToUniversalTime(),
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
                ExamComplementaryId: dto.ExamComplementary != null ? Guid.Parse(dto.ExamComplementary) : null
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
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _mediator.Send(new GetAllConsultQuery());
            return Ok(result.Value);
        }
        catch (Exception ex)
        {
            return Problem(ErrorHelper.GetExceptionError(ex));
        }
    }
}