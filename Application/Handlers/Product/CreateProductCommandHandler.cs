using Application.Commands.Inventory;
using Application.Commands.Product;
using Application.Dto.Response.Product;
using Application.Helpers;
using Application.Specifications.Category;
using Ardalis.Result;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Product;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductBasicResDto>>
{
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<CategoryEntity> _categoryRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public CreateProductCommandHandler(IAsyncRepository<ProductEntity> productRepository, IAsyncRepository<CategoryEntity> categoryRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository, IMapper mapper, IAsyncRepository<UserEntity> userRepository, IMediator mediator)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<Result<ProductBasicResDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userCreator = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userCreator is null || userCreator.ShopId is null)
            {
                return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var category = await _categoryRepository.FirstOrDefaultAsync(new GetCategoryByIdAndShopSpecification(request.CategoryId, (Guid)userCreator.ShopId), cancellationToken);

            if (category is null)
            {
                return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Category not found",}
                });
            }

            var product = new ProductEntity(
                request.Name,
                request.Price,
                0,
                category.Id,
                description: request.Description,
                shopId: userCreator.ShopId
            );

            if (request.ImageBase64 is not null)
            {

                var imageUrl = await _uploaderRepository.Upload(request.ImageBase64);

                if (imageUrl is null)
                {
                    return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Image not uploaded",}
                });
                }

                var imageEntity = await _imageRepository.AddAsync(new ImageEntity(
                    imageUrl,
                    imageUrl
                ), cancellationToken);

                product.ImageId = imageEntity.Id;
            }


            product.SetCreationInfo(request.UserId);
            await _productRepository.AddAsync(product, cancellationToken);

            var resultAddHistory = await _mediator.Send(new AddInventoryHistoryCommand(
                Guid.Parse(TypeMovementConst.Input),
                product.Id,
                request.Stock,
                request.UserId
            ), cancellationToken);

            if (resultAddHistory.IsInvalid())
            {
                return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Error adding inventory history",}
                });
            }

            var mapper = _mapper.Map<ProductBasicResDto>(product);

            return Result.Success(mapper);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
