using Application.Dto.Request.Category;
using Application.Specifications.Category;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Category;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Result<CategoryEntity>>
{
    private readonly IAsyncRepository<CategoryEntity> _repository;
    private readonly IAsyncRepository<UserEntity> _userRepository;

    public UpdateCategoryHandler(IAsyncRepository<CategoryEntity> repository, IAsyncRepository<UserEntity> userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<Result<CategoryEntity>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userEntity is null || userEntity.ShopId is null)
        {
            return Result<CategoryEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Shop not found",}
            });
        }

        var category = await _repository.FirstOrDefaultAsync(new GetCategoryByIdAndShopSpecification(request.Id, (Guid)userEntity.ShopId), cancellationToken);

        if (category is null)
        {
            return Result<CategoryEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Category not found",}
            });
        }

        category.Name = request.Name;
        category.SetUpdateInfo(request.UserId);

        await _repository.UpdateAsync(category, cancellationToken);

        return Result.Success(category);
    }
}
