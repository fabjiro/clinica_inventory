using Application.Dto.Response.Inventory;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Inventory;

public record AddInventoryHistoryCommand(Guid TypeMovement, Guid ProductId, int Quantity, string UserId) : IRequest<Result<InventoryHistoryResponse>>; 