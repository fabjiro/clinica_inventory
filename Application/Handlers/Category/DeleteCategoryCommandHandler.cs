using Application.Commands.Category;
using Application.Specifications.Category;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace  Application.Handlers.Category;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result<CategoryEntity>>
{
    private readonly IAsyncRepository<CategoryEntity> _categoryRepository;
    private readonly IAsyncRepository<UserEntity> _userRepository;

    public DeleteCategoryCommandHandler(IAsyncRepository<CategoryEntity> categoryRepository, IAsyncRepository<UserEntity> userRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<Result<CategoryEntity>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userEntity is null || userEntity.ShopId is null)
        {
            return Result<CategoryEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Shop not found",}
            });
        }

        var category = await _categoryRepository.FirstOrDefaultAsync(new GetCategoryByIdAndShopSpecification(
            request.Id,
            (Guid)userEntity.ShopId
        ), cancellationToken);

        if (category is null)
        {
            return Result<CategoryEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Category not found",}
            });
        }

        category.SetDeletedInfo(request.UserId);
        await _categoryRepository.UpdateAsync(category, cancellationToken);

        return Result.Success(category);
    }
}
