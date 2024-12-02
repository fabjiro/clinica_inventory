using Application.Commands.Product;
using Application.Dto.Response.Product;
using Application.Helpers;
using Application.Specifications.Product;
using Ardalis.Result;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Product;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductBasicResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IAsyncRepository<CategoryEntity> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IUploaderRepository _uploaderRepository;
    public UpdateProductCommandHandler(IAsyncRepository<ProductEntity> productRepository, IAsyncRepository<ImageEntity> imageRepository, IMapper mapper, IUploaderRepository uploaderRepository, IAsyncRepository<CategoryEntity> categoryRepository, IAsyncRepository<UserEntity> userRepository)
    {
        _productRepository = productRepository;
        _imageRepository = imageRepository;
        _mapper = mapper;
        _uploaderRepository = uploaderRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ProductBasicResDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null || userEntity.ShopId is null)
            {
                return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var product = await _productRepository.FirstOrDefaultAsync(new GetProductByIdAndShopSpecification(
                          request.Id,
                          (Guid)userEntity.ShopId
                      ), cancellationToken);

            if (product is null)
            {
                return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Product not found",}
                });
            }

            if (request.Name is not null && request.Name != product.Name)
            {
                product.Name = request.Name;
            }

            if (request.Description is not null && request.Description != product.Description)
            {
                product.Description = request.Description;
            }

            if (request.Price is not null)
            {
                product.Price = (decimal)request.Price;
            }

            if (request.Stock is not null)
            {
                product.Stock = (int)request.Stock;
            }

            if (request.CategoryId is not null && request.CategoryId != product.CategoryId)
            {
                var category = await _categoryRepository.GetByIdAsync((Guid)request.CategoryId, cancellationToken);

                if (category is null)
                {
                    return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Category not found",}
                    });
                }

                product.CategoryId = category.Id;
            }

            if (request.Image is not null)
            {
                var image = await _imageRepository.GetByIdAsync(product.ImageId, cancellationToken);

                if (image is null)
                {
                    return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not found",}
                    });
                }

                var imageUrl = await _uploaderRepository.Upload(request.Image);

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
                await _productRepository.UpdateAsync(product, cancellationToken);

                if (image.Id != Guid.Parse(DefaulConst.DefaultImageProduct))
                {
                    await Task.WhenAll([
                        _uploaderRepository.Delete(image.OriginalUrl),
                        _imageRepository.DeleteAsync(image, cancellationToken),
                    ]);
                }

            }

            product.SetUpdateInfo(request.UserId);
            await _productRepository.UpdateAsync(product, cancellationToken);

            return Result.Success(_mapper.Map<ProductBasicResDto>(product));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}