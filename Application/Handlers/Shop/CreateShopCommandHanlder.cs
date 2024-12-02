using Application.Commands.Shop;
using Application.Dto.Response.Shop;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Attributes;
using Domain.Entities.Shop;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Shop;

public class CreateShopCommandHanlder : IRequestHandler<CreateShopCommand, Result<ShopResponseDto>>
{
    private readonly IAsyncRepository<ShopEntity> _shopEntityRepository;
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IAsyncRepository<AttributesEntity> _attributeRepository;
    private readonly IAsyncRepository<ShopTypeEntity> _shopTypeRepository;

    public CreateShopCommandHanlder(IAsyncRepository<ShopEntity> shopRepository, IAsyncRepository<AttributesEntity> attributeRepository, IAsyncRepository<ShopTypeEntity> shopTypeRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository, IMapper mapper)
    {
        _shopEntityRepository = shopRepository;
        _attributeRepository = attributeRepository;
        _shopTypeRepository = shopTypeRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
        _mapper = mapper;
    }


    public async Task<Result<ShopResponseDto>> Handle(CreateShopCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var shopType = await _shopTypeRepository.GetByIdAsync(request.ShopTypeId, cancellationToken);

            if (shopType is null)
            {
                return Result<ShopResponseDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop type not found",}
                });
            }

            var attribute = await _attributeRepository.GetByIdAsync(request.AttributeId, cancellationToken);

            if (attribute is null)
            {
                return Result<ShopResponseDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Attribute not found",}
                });
            }

            var shopEntity = new ShopEntity(
                request.Name,
                request.MinStockProducts,
                request.AttributeId,
                request.ShopTypeId,
                null
            );

            if (request.Image is not null)
            {

                var imageUrl = await _uploaderRepository.Upload(request.Image);

                if (imageUrl is null)
                {
                    return Result<ShopResponseDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not uploaded",}
                    });
                }

                var image = await _imageRepository.AddAsync(new ImageEntity(imageUrl, imageUrl), cancellationToken);

                if (image is null)
                {
                    return Result<ShopResponseDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not uploaded",}
                    });
                }

                shopEntity.LogoId = image.Id;
            }


            shopEntity.SetCreationInfo(request.UserId);

            await _shopEntityRepository.AddAsync(shopEntity, cancellationToken);

            var response = _mapper.Map<ShopResponseDto>(shopEntity);
            return Result<ShopResponseDto>.Success(response);

        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
