using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Querys.Report;
using Application.Specifications.Consult;
using Ardalis.Result;
using AutoMapper;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportRecentDiagnosticsQueryHandler : IRequestHandler<ReportRecentDiagnosticsQuery, Result<List<ReportRecentConsultResDto>>>
{
    private readonly IAsyncRepository<ConsultEntity> _consultRepository;
    private readonly IMapper _mapper;
    public ReportRecentDiagnosticsQueryHandler(IAsyncRepository<ConsultEntity> consultRepository, IMapper mapper)
    {
        _consultRepository = consultRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReportRecentConsultResDto>>> Handle(ReportRecentDiagnosticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var consults = await _consultRepository.ListAsync(new GetConsultByRangeDateSpcecifications(
                startDate: request.StartDate,
                endDate: request.EndDate,
                include: true
            ), cancellationToken);
            
            var consultsDto = _mapper.Map<List<ReportRecentConsultResDto>>(consults);
            return Result.Success(consultsDto);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}