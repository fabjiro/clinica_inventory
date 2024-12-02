using Application.Commands.Category;
using Application.Helpers;
using Application.Specifications.User;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Category;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryEntity>>
{
    private readonly IAsyncRepository<CategoryEntity> _repository;
    private readonly IAsyncRepository<UserEntity> _userRepository;

    public CreateCategoryCommandHandler(IAsyncRepository<CategoryEntity> repository, IAsyncRepository<UserEntity> UserRepository)
    {
        _repository = repository;
        _userRepository = UserRepository;
    }

    public async Task<Result<CategoryEntity>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null  || userEntity.ShopId is null)
            {
                return Result<CategoryEntity>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "User not found",}
                });
            }

            var category = new CategoryEntity(request.Name, userEntity.ShopId);
            category.SetCreationInfo(request.UserId);

            await _repository.AddAsync(category, cancellationToken);
            return category;
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}