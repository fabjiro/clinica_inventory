using Application.Dto.Response.Product;
using Application.Helpers;
using Application.Queries.Product;
using Application.Specifications.Product;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Product;

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, Result<List<ProductBasicResDto>>>
{
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public SearchProductQueryHandler(IAsyncRepository<ProductEntity> productRepository, IAsyncRepository<UserEntity> userRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductBasicResDto>>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null || userEntity.ShopId is null)
            {
                return Result<List<ProductBasicResDto>>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var products = await _productRepository.ListAsync(new SearchProductSpecification(
                (Guid)userEntity.ShopId,
                request.Word
            ), cancellationToken);

            Console.WriteLine(products.Count);

            var mapper = _mapper.Map<List<ProductBasicResDto>>(products);
            
            return Result.Success(mapper);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}