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
using Microsoft.Extensions.Configuration;

namespace Application.Handlers.User;
public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<UserBasicResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly IAsyncRepository<RolEntity> _rolRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IUploaderRepository _uploaderRepository;

    public AddUserCommandHandler(IMapper mapper, IAsyncRepository<UserEntity> userRepository, IAsyncRepository<RolEntity> rolRepository, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _rolRepository = rolRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
    }


    public async Task<Result<UserBasicResDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {

        try
        {

            var userByEmail = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpecifications(
                request.Email
            ), cancellationToken);

            if (userByEmail != null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Email already exists",}
                });
            }

            var userEntity = new UserEntity(
                request.Name,
                request.Email,
                PasswordHelper.HashPassword(request.Password!),
                request.RolId!.Value
            );


            if (request.Avatar is not null)
            {
                var url = await _uploaderRepository.Upload(request.Avatar);

                if (url is null)
                {
                    return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "Error uploading avatar",}
                });
                }

                var imageEntity = await _imageRepository.AddAsync(new ImageEntity(
                    url,
                    url
                ), cancellationToken);

                userEntity.AvatarId = imageEntity.Id;
            }

            userEntity.SetCreationInfo(request.UserId);

            await _userRepository.AddAsync(userEntity, cancellationToken);

            var newUser = await _userRepository.FirstOrDefaultAsync(new GetUserByIdIncludesSpecifications(userEntity.Id), cancellationToken);

            return Result.Success(_mapper.Map<UserBasicResDto>(newUser));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
