using Application.Commands.Product;
using Application.Dto.Response.Product;
using Application.Helpers;
using Application.Specifications.Product;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Product;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<ProductBasicResDto>>
{
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<UserEntity> _userRepository;

    public DeleteProductCommandHandler(IAsyncRepository<ProductEntity> productRepository, IMapper mapper, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository, IAsyncRepository<UserEntity> userRepository)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<ProductBasicResDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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

            // var image = await _imageRepository.GetByIdAsync(product.ImageId, cancellationToken);

            // if (image is null)
            // {
            //     return Result<ProductBasicResDto>.Invalid(new List<ValidationError> {
            //         new () {ErrorMessage = "Image not found",}
            //     });
            // }
            
            product.SetDeletedInfo(request.UserId);
            await _productRepository.UpdateAsync(product, cancellationToken);

            // if(product.ImageId != Guid.Parse(DefaulConst.DefaultImageProduct))
            // {
            //     await Task.WhenAll([
            //         _uploaderRepository.Delete(image.OriginalUrl),
            //         _imageRepository.DeleteAsync(image, cancellationToken),
            //     ]);                
            // }


            return Result.Success(_mapper.Map<ProductBasicResDto>(product));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
