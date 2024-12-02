using Application.Commands.Inventory;
using Application.Dto.Response.Inventory;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Inventory;

public class AddInventoryHistoryCommandHanlder : IRequestHandler<AddInventoryHistoryCommand, Result<InventoryHistoryResponse>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<ProductEntity> _productRepository;
    private readonly IAsyncRepository<InventoryHistoryEntity> _repository;
    private readonly IMapper _mapper;

    public AddInventoryHistoryCommandHanlder(IAsyncRepository<UserEntity> userRepository, IAsyncRepository<ProductEntity> productRepository, IAsyncRepository<InventoryHistoryEntity> repository, IMapper mapper)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<InventoryHistoryResponse>> Handle(AddInventoryHistoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null || userEntity.ShopId is null)
            {
                return Result<InventoryHistoryResponse>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var productEntity = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

            if (productEntity is null)
            {
                return Result<InventoryHistoryResponse>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Product not found",}
                });
            }

            var inventoryHistoryEntity = new InventoryHistoryEntity(
                (Guid)userEntity.ShopId,
                request.ProductId,
                request.TypeMovement,
                request.Quantity,
                productEntity.Price,
                userEntity.Id
            );

            if(request.Quantity < 0) {
                return Result<InventoryHistoryResponse>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Stock not enough",}
                });
            }

            if (request.TypeMovement == Guid.Parse(TypeMovementConst.Input))
            {
                productEntity.Stock += request.Quantity;
            } else if (request.TypeMovement == Guid.Parse(TypeMovementConst.Output))
            {
                productEntity.Stock -= request.Quantity;
            } else if (request.TypeMovement == Guid.Parse(TypeMovementConst.Adjustment))
            {
                productEntity.Stock = request.Quantity;
            }

            if(productEntity.Stock < 0)
            {
                return Result<InventoryHistoryResponse>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Stock not enough",}
                });
            }

            await _productRepository.UpdateAsync(productEntity, cancellationToken);
            await _repository.AddAsync(inventoryHistoryEntity, cancellationToken);

            return Result.Success(_mapper.Map<InventoryHistoryResponse>(inventoryHistoryEntity));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
