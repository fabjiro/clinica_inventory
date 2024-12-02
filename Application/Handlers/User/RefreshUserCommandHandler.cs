using Application.Commands.User;
using Application.Dto.User;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.User;

public class RefreshUserCommandHandler : IRequestHandler<RefreshUserCommand, Result<AuthUserResDto>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public RefreshUserCommandHandler(IAsyncRepository<UserEntity> userRepository, IJwtService jwtService, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public async Task<Result<AuthUserResDto>> Handle(RefreshUserCommand request, CancellationToken cancellationToken)
    {
        var validRefreshToken = _jwtService.ValidateToken(request.refreshToken);

        if(!validRefreshToken)
        {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Invalid refresh token",}
            });
        }

        var userId = _jwtService.GetUserIdFromToken(request.refreshToken);

        if(userId is null)
        {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "User id not found",}
            });
        }

        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken);

        if(user is null) {
            return Result<AuthUserResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "User not found",}
            });
        }

        var token = _jwtService.GenerateToken(user.Id.ToString());
        var refreshToken = _jwtService.GenerateToken(user.Id.ToString(), 56);

        var mapper = _mapper.Map<AuthUserResDto>(user);
        mapper.Token = token;
        mapper.RefreshToken = refreshToken;

        return Result.Success(mapper);
    }
}
