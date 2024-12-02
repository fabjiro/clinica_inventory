using Application.Dto.Response.Category;
using Application.Queries.Category;
using Application.Specifications.Category;
using Application.Specifications.User;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Category;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<CategoryResDto>>
{
    private readonly IAsyncRepository<CategoryEntity> _categoryAsyncRepository;
    private readonly IAsyncRepository<UserEntity> _userAsyncRepository;
    private readonly IMapper _mapper;

    public GetCategoryQueryHandler(IAsyncRepository<CategoryEntity> categoryAsyncRepository, IMapper mapper, IAsyncRepository<UserEntity> userAsyncRepository)
    {
        _categoryAsyncRepository = categoryAsyncRepository;
        _mapper = mapper;
        _userAsyncRepository = userAsyncRepository;
    }

    public async Task<List<CategoryResDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _userAsyncRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userEntity is null || userEntity.ShopId is null)
        {
            return [];
        }

        // Recuperar todas las categorías desde el repositorio
        var categories = await _categoryAsyncRepository.ListAsync(new GetAllCategoryByShopIdSpecifications(
            (Guid)userEntity.ShopId
        ), cancellationToken);
        
        return _mapper.Map<List<CategoryResDto>>(categories);
    }
}