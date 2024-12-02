using Application.Dto.Response.Inventory;
using Application.Helpers;
using Application.Queries.Inventory;
using Application.Specifications.Inventory;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Inventory;

public class GetAllInventoryHistoryQueryHandler : IRequestHandler<GetAllInventoryHistoryQuery, Result<List<InventoryHistoryResponse>>>
{
    private readonly IAsyncRepository<InventoryHistoryEntity> _repository;
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;

    public GetAllInventoryHistoryQueryHandler(IAsyncRepository<InventoryHistoryEntity> repository, IAsyncRepository<UserEntity> userRepository, IMapper mapper)
    {
        _repository = repository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<InventoryHistoryResponse>>> Handle(GetAllInventoryHistoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(request.IdUser, cancellationToken);

            if (userEntity is null || userEntity.ShopId is null)
            {
                return Result<List<InventoryHistoryResponse>>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var result = await _repository.ListAsync(new InventoryHistoryIncludeSpecification(
                idShop: (Guid)userEntity.ShopId
            ),cancellationToken);

            var mapper = _mapper.Map<List<InventoryHistoryResponse>>(result);
            return Result.Success(mapper);   
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}