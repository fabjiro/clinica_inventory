using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;

namespace Domain.Entities.Shop;

[Table("shop_type")]
public class ShopTypeEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ShopTypeEntity() {}

    public ShopTypeEntity(string name) {
        Id = Guid.NewGuid();
        Name = name;
    }
} 