using Application.Commands.User;
using Application.Dto.Response.User;
using Application.Helpers;
using Application.Specifications.Patient;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Patient;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, Result<UserBasicResDto>>
{
    private readonly IAsyncRepository<PatientEntity> _userRepository;
    private readonly IUploaderRepository _uploaderRepository;
    private readonly IAsyncRepository<ImageEntity> _imageRepository;
    private readonly IMapper _mapper;
    public AddPatientCommandHandler(IAsyncRepository<PatientEntity> userRepository, IMapper mapper, IAsyncRepository<ImageEntity> imageRepository, IUploaderRepository uploaderRepository)
    {
        _userRepository = userRepository;
        _imageRepository = imageRepository;
        _uploaderRepository = uploaderRepository;
        _mapper = mapper;
    }
    public async Task<Result<UserBasicResDto>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var isUserExit = await _userRepository.FirstOrDefaultAsync(new GetPatientByIdentification(request.Identification), cancellationToken);

            if (isUserExit is not null)
            {
                return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "User already exists",}
                });
            }

            var userEntity = new PatientEntity(
                request.Name,
                rolId: Guid.Parse(RolConst.Consultation),
                civilStatusId: request.CivilStatus,
                typeSex: Guid.Parse(TypeSexConst.Female),
                identification: request.Identification,
                phone: request.Phone,
                address: request.Address,
                age: request.Age,
                contactPerson: request.ContactPerson,
                contactPhone: request.ContactPhone,
                birthday: request.Birthday,
                avatarId: Guid.Parse(DefaulConst.DefaultAvatarUserId)
            );


            if (request.Avatar is not null)
            {
                var image = await _uploaderRepository.Upload(request.Avatar);

                if (image is null)
                {
                    return Result<UserBasicResDto>.Invalid(new List<ValidationError> {
                        new () {ErrorMessage = "Image not uploaded",}
                    });
                }

                var newImage = await _imageRepository.AddAsync(new ImageEntity
                {
                    CompactUrl = image,
                    OriginalUrl = image
                }, cancellationToken);

                userEntity.AvatarId = newImage.Id;
            }

            var newUser = await _userRepository.AddAsync(userEntity, cancellationToken);

            return Result.Success(_mapper.Map<UserBasicResDto>(newUser));

        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }

}