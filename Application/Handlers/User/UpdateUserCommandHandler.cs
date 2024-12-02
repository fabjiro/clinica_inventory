using Application.Commands.User;
using Application.Dto.Response.User;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.User;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserBasicResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<RolEntity> _rolRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IMapper mapper, IAsyncRepository<UserEntity> userRepository, IAsyncRepository<RolEntity> rolRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _rolRepository = rolRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
    }

    public async Task<Result<UserBasicResDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userUpdater = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

        if (userUpdater is null || userUpdater.ShopId is null)
        {
            return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Shop not found",}
            });
        }

        var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdAndShopSpecifications(request.id, (Guid)userUpdater.ShopId), cancellationToken);

        if (user is null)
        {
            return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "User not found",}
            });
        }

        // update name
        if (request.name is not null && request.name != user.Name)
        {
            user.Name = request.name;
        }

        // update password
        if (request.password is not null && request.password != user.Password)
        {
            var passwordHash = PasswordHelper.HashPassword(request.password);
            user.Password = passwordHash;
        }

        // update rol
        if (request.rolId is not null && request.rolId != user.RolId)
        {
            var rol = await _rolRepository.GetByIdAsync(request.rolId.Value);

            if (rol is null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Rol not found",}
                });
            }
            user.RolId = request.rolId.Value;
        }

        // update avatar
        if (request.Avatar is not null)
        {

            var avatarUrl = await _uploaderRepository.Upload(request.Avatar);
            if (avatarUrl is null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Avatar not uploaded",}
                });
            }

            var image = await _imageRepository.AddAsync(new ImageEntity(
                avatarUrl,
                avatarUrl
            ), cancellationToken);


            if (user.AvatarId != Guid.Parse(DefaulConst.DefaultAvatarUserId))
            {
                // delete register db
                var imageEntity = await _imageRepository.GetByIdAsync(user.AvatarId, cancellationToken);

                if (imageEntity is null)
                {
                    return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not found",}
                    });
                }

                user.AvatarId = image.Id;
                await _userRepository.UpdateAsync(user, cancellationToken);


                await Task.WhenAll([
                    _imageRepository.DeleteAsync(imageEntity, cancellationToken),
                    _uploaderRepository.Delete(imageEntity.OriginalUrl)
                ]);

            }
            else
            {
                // new image
                user.AvatarId = image.Id;
            }

        }

        user.SetUpdateInfo(request.UserId);
        await _userRepository.UpdateAsync(user, cancellationToken);

        var newData = await _userRepository.FirstOrDefaultAsync(new GetUserByIdIncludesSpecifications(user.Id), cancellationToken);

        return Result.Success(_mapper.Map<UserBasicResDto>(newData));
    }
}
