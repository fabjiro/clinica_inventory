using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specifications.Inventory;

public class InventoryHistoryIncludeSpecification : Specification<InventoryHistoryEntity>
{
    public InventoryHistoryIncludeSpecification(Guid? idShop = null)
    {
        Query.Include(x => x.Product);
        Query.Include(x => x.Product.Image);
        Query.Include(x => x.User);
        Query.Include(x => x.User.Avatar);

        Query.OrderByDescending(x => x.CreatedAt);

        if (idShop is not null)
        {
            Query.Where(x => x.IdShop == idShop);
        }
    }
}