using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Shop;

namespace Domain.Entities;

[Table("inventory_history")]
public class InventoryHistoryEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid IdShop { get; set; }

    [ForeignKey("IdShop")]
    public ShopEntity? Shop { get; set; }

    [Required]
    public Guid IdProducto { get; set; }

    [ForeignKey("IdProducto")]
    public ProductEntity? Product { get; set; }

    [Required]    
    public Guid IdUser { get; set; }

    [ForeignKey("IdUser")]
    public UserEntity? User { get; set; }

    public Guid? TypeMovement { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public int Count { get; set; }
    public decimal PriceUnit { get; set; }

    public InventoryHistoryEntity() { }

    public InventoryHistoryEntity(Guid shopId ,Guid productId, Guid typeMovement, int count, decimal priceUnit, Guid userId)
    {
        Id = Guid.NewGuid();
        IdUser = userId;
        IdProducto = productId;
        TypeMovement = typeMovement;
        Count = count;
        PriceUnit = priceUnit;
        CreatedAt = DateTime.UtcNow;
        IdShop = shopId;
    }

}