using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Const;

namespace Domain.Entities.Shop;

[Table("shop")]
public class ShopEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int MinStockProducts { get; set; }
    public Guid LogoId { get; set; }

    [ForeignKey("LogoId")]
    public ImageEntity? Logo { get; set; }

    public Guid ShopTypeId { get; set; }

    [ForeignKey("ShopTypeId")]
    public ShopTypeEntity? ShopType { get; set; }

    public ShopEntity() {}

    public ShopEntity(string name, int minStockProducts, Guid shopTypeId, Guid? logoId) {
        Id = Guid.NewGuid();
        Name = name;
        MinStockProducts = minStockProducts;
        LogoId = logoId ?? Guid.Parse(DefaulConst.DefaultImageShop);
        ShopTypeId = shopTypeId;
    }
}