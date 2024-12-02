using Application.Dto.Response.Attributes;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Attributes;
public class GetAllAttributesQuery : IRequest<Result<List<AttributesBasicResDto>>> {};