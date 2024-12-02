using Application.Commands.User;
using Application.Dto.User;
using Application.Specifications.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthUserResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;


    public LoginUserCommandHandler(IAsyncRepository<UserEntity> userRepository, IMapper mapper, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }


    public async Task<Result<AuthUserResDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userByEmail = await _userRepository.FirstOrDefaultAsync(new GetUserByEmailSpecifications(request.Email), cancellationToken);

        if (userByEmail is null)
        {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Verifique sus credenciales",}
            });
        }

        if(!PasswordHelper.VerifyPassword(request.Password, userByEmail.Password))
        {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Verifique sus credenciales",}
            });
        }

        var mapper = _mapper.Map<AuthUserResDto>(userByEmail);
        mapper.Token = _jwtService.GenerateToken(userByEmail.Id.ToString());
        mapper.RefreshToken = _jwtService.GenerateToken(userByEmail.Id.ToString(), 56);

        return Result.Success(mapper);
    }
}
