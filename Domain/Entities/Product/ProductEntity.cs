using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Const;
using Domain.Entities.Attributes;
using Domain.Entities.Shop;

namespace Domain.Entities;

[Table("product")]
public class ProductEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int Stock { get; set; } = 0;

    public Guid? ShopId { get; set; }

    [ForeignKey("ShopId")]
    public ShopEntity? Shop { get; set; }

    public Guid? AttributeValueId { get; set; }

    [ForeignKey("AttributeValueId")]
    public AttributesValuesEntity? AttributeValue { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    
    [Required]
    [ForeignKey("CategoryId")]
    public CategoryEntity? Category { get; set; }

    [Required]
    public Guid ImageId { get; set; }
    
    [Required]
    [ForeignKey("ImageId")]
    public ImageEntity? Image { get; set; }

    public ProductEntity() {}

    public ProductEntity(string name, decimal price, int stock, Guid categoryId, string? description = null, Guid? imageId = null, Guid? shopId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageId = imageId ?? Guid.Parse(DefaulConst.DefaultImageProduct);
        ShopId = shopId;
    }
}