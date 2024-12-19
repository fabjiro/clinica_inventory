using Application.Dto.Response.Group;
using MediatR;

namespace Application.Queries.Group;

public record GetAllGroupQuery : IRequest<List<GroupDto>>;
