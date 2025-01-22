using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Consult;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportDiagnosticsQueryHandler : IRequestHandler<ReportDiagnosticsQuery, Result<List<DiagnosticsResDto>>>
{
    private readonly IAsyncRepository<ConsultEntity> _consutlRepository;
    private readonly IMapper _mapper;

    public ReportDiagnosticsQueryHandler(IAsyncRepository<ConsultEntity> consutlRepository, IMapper mapper)
    {
        _consutlRepository = consutlRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<DiagnosticsResDto>>> Handle(ReportDiagnosticsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var consults = await _consutlRepository.ListAsync(
                new GetConsultByRangeDateSpcecifications(
                    startDate: request.StartDate,
                    endDate: request.EndDate,
                    include: true
                ),
                cancellationToken
            );

            var diagnostics = _mapper.Map<List<DiagnosticsResDto>>(consults);

            return Result.Success(diagnostics);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}