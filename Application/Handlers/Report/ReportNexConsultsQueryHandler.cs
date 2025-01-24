using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Consult;
using Ardalis.Result;
using AutoMapper;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportNexConsultsQueryHandler : IRequestHandler<ReportNexConsultsQuery, Result<List<ReportNextConsultsResDto>>>
{
    private readonly IAsyncRepository<ConsultEntity> _consultRepository;
    private readonly IMapper _mapper;

    public ReportNexConsultsQueryHandler(IAsyncRepository<ConsultEntity> consultRepository, IMapper mapper)
    {
        _consultRepository = consultRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ReportNextConsultsResDto>>> Handle(ReportNexConsultsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var consults = await _consultRepository.ListAsync(new GetConsultByRangeDateSpcecifications(
                startDate: request.StartDate,
                endDate: request.EndDate,
                include: true
            ), cancellationToken);

            var dateNow = DateTime.UtcNow;

            // seleccionar las consultas que tengan fecha de cita mayor a la fecha actual
            var result = consults.Where(i => i.Nextappointment > dateNow).ToList();

            return Result.Success(_mapper.Map<List<ReportNextConsultsResDto>>(result));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}