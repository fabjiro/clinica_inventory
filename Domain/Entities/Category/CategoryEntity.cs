using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Entities.Shop;

namespace Domain.Entities;

[Table("category")]
public class CategoryEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = String.Empty;

    public Guid? ShopId { get; set; }

    [ForeignKey("ShopId")]
    public ShopEntity? Shop { get; set; }
    
    public ICollection<ProductEntity> Products { get; set; } = new List<ProductEntity>();

    public CategoryEntity() { }
    public CategoryEntity(string name, Guid? shopId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ShopId = shopId;
    }
}