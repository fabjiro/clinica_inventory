using Application.Dto.Response.Inventory;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Inventory;

public record GetAllInventoryHistoryQuery(Guid IdUser) : IRequest<Result<List<InventoryHistoryResponse>>> {};