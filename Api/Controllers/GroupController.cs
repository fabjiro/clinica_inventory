using Application.Dto.Response.Group;
using Application.Queries.Group;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<GroupDto>>> GetAllGroup()
    {
        var groups = await _mediator.Send(new GetAllGroupQuery());
        return Ok(groups);
    }
}