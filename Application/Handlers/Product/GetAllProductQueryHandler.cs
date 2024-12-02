using Application.Dto.Response.Product;
using Application.Queries.Product;
using Application.Specifications.Product;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Product;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, Result<List<ProductBasicResDto>>>
{
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IAsyncRepository<ProductEntity> productRepository, IMapper mapper, IAsyncRepository<UserEntity> userRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task<Result<List<ProductBasicResDto>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userEntity is null || userEntity.ShopId is null)
        {
            return Result<List<ProductBasicResDto>>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Shop not found",}
            });
        }
        
        var products = await _productRepository.ListAsync(new GetAllProductIncludeSpecification(
            shopId: (Guid)userEntity.ShopId
        ),cancellationToken);
        
        var mapper = _mapper.Map<List<ProductBasicResDto>>(products);
        return Result.Success(
            mapper
        );
    }
}
