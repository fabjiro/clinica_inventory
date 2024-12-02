using Application.Dto.Response.Product;
using Application.Dto.Response.User;

namespace Application.Dto.Response.Inventory;

public class InventoryHistoryResponse
{
    public Guid Id { get; set; }
    public ProductBasicResDto? Product { get; set; }
    public int Count { get; set; }
    public decimal PriceUnit { get; set; }
    public string? TypeMovement { get; set; } 
    public UserBasicResDto? User { get; set; }
    public DateTime CreatedAt { get; set; }

}