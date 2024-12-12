using Application.Commands.User;
using Application.Dto.User;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.User;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<AuthUserResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public RegisterUserHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }
    public async Task<Result<AuthUserResDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpecifications(request.email), cancellationToken);
        if (userByEmail != null)
        {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Email already exists",}
            });
        }

        var hashedPassword = PasswordHelper.HashPassword(request.password);

        var categoryEntity = new UserEntity(
            request.fullname,
            request.email,
            hashedPassword,
            rolId:Guid.Parse(RolConst.Consultation),
            null
        );

        categoryEntity.SetCreationInfo("");

        var newUser = await _userRepository.AddAsync(categoryEntity, cancellationToken);

        var mapper = _mapper.Map<AuthUserResDto>(newUser);
        mapper.Token = _jwtService.GenerateToken(newUser.Id.ToString());

        return Result.Success(mapper);
    }
}
