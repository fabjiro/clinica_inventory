using Application.Commands.User;
using Application.Dto.Response.User;
using Application.Helpers;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.User;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<UserBasicResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IMapper _mapper;

    public DeleteUserCommandHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
    }

    public async Task<Result<UserBasicResDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var userDeleter = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userDeleter is null || userDeleter.ShopId is null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Shop not found",}
                });
            }

            var userEntity = await _userRepository.FirstOrDefaultAsync(new GetUserByIdAndShopSpecifications(
                request.Id,
                (Guid)userDeleter.ShopId
            ), cancellationToken);

            if (userEntity is null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "User not found",}
                });
            }

            // if(userEntity.AvatarId != Guid.Parse(DefaulConst.DefaultAvatarUserId)) 
            // {
            //     var image = await _imageRepository.GetByIdAsync(userEntity.AvatarId, cancellationToken);

            //     if (image is not null)
            //     {
            //         await _uploaderRepository.Delete(image.OriginalUrl);
            //         // await _uploaderRepository.Delete(image.CompactUrl);
            //         await _imageRepository.DeleteAsync(image, cancellationToken);
            //     }
            // }

            userEntity.SetDeletedInfo(request.UserId);
            await _userRepository.UpdateAsync(userEntity, cancellationToken);

            return Result.Success(_mapper.Map<UserBasicResDto>(userEntity));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
