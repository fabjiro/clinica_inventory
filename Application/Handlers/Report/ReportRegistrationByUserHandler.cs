using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Consult;
using Application.Specifications.Patient;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportRegistrationByUserHandler : IRequestHandler<ReportRegistrationByUser, Result<List<ReportRegistrationByUserResDto>>>
{
    private readonly IAsyncRepository<UserEntity> _userRepository;
    private readonly IAsyncRepository<ConsultEntity> _consultRepository;
    private readonly IMapper _mapper;

    public ReportRegistrationByUserHandler(IMapper mapper, IAsyncRepository<UserEntity> userRepository, IAsyncRepository<ConsultEntity> consultRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _consultRepository = consultRepository;
    }

    public async Task<Result<List<ReportRegistrationByUserResDto>>> Handle(ReportRegistrationByUser request, CancellationToken cancellationToken)
    {
        try
        {
            var userEntity = await _userRepository.GetByIdAsync(Guid.Parse(request.UserId), cancellationToken);

            if (userEntity is null || userEntity.IsDeleted)
            {
                return Result.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "User not found",}
                });
            }

            var consults = await _consultRepository.ListAsync(
                new GetConsultByRangeDateSpcecifications(
                    startDate: request.StartDate,
                    endDate: request.EndDate,
                    userId: userEntity.Id.ToString(),
                    include: true
                ),
                cancellationToken
            );

            var consultsDto = _mapper.Map<List<ReportRegistrationByUserResDto>>(consults);

        
            return Result.Success(consultsDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}