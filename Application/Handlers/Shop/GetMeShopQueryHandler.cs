using Application.Dto.Response.Shop;
using Application.Queries.Shop;
using Application.Specifications.Shop;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Shop;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Shop;

public class GetMeShopQueryHandler : IRequestHandler<GetMeShopQuery, Result<ShopResponseDto>>
{
    private readonly IAsyncRepository<ShopEntity> _shopRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetMeShopQueryHandler(IAsyncRepository<ShopEntity> shopRepository, IMapper mapper, IAsyncRepository<UserEntity> userRepository)
    {
        _shopRepository = shopRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Result<ShopResponseDto>> Handle(GetMeShopQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userEntity is null || userEntity.ShopId is null)
        {
            return Result<ShopResponseDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Shop not found",}
            });
        }
        var shop = await _shopRepository.FirstOrDefaultAsync(new ShopIncludesSpecifications((Guid)userEntity.ShopId), cancellationToken);
        return Result.Success(_mapper.Map<ShopResponseDto>(shop));
    }
}