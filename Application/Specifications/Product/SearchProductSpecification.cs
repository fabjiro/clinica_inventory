using Application.Dto.Response.Image;
using Application.Dto.Response.Product;
using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications.Product;

public class SearchProductSpecification : Specification<ProductEntity>
{
    public SearchProductSpecification(Guid ShopId, string search)
    {
        var trimmedSearch = search.ToLower();

        Query.Where(x => x.ShopId == ShopId);
        Query.Where(x => x.IsDeleted == false);
        Query.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{trimmedSearch}%") || EF.Functions.Like((x.Description ?? "").ToLower(), $"%{trimmedSearch}%"));
        Query.Include(x => x.Category);
        Query.Include(x => x.Image);
        Query.Include(x => x.AttributeValue);
    }
}