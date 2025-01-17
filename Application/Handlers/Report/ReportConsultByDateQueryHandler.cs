using Application.Dto.Response.Report;
using Application.Helpers;
using Application.Queries.Report;
using Application.Specifications.Consult;
using Ardalis.Result;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Report;

public class ReportConsultByDateQueryHandler : IRequestHandler<ReportConsultByDateQuery, Result<List<ReportConsultByDateDto>>>
{
    private readonly IAsyncRepository<ConsultEntity> _consultRepository;

    public ReportConsultByDateQueryHandler(IAsyncRepository<ConsultEntity> consultRepository)
    {
        _consultRepository = consultRepository;
    }

    public async Task<Result<List<ReportConsultByDateDto>>> Handle(ReportConsultByDateQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // get utc now date
            var to = DateTime.UtcNow;
            var from = to.AddDays(-7);


            var consults = await _consultRepository.ListAsync(new GetReportConsultByDateSpecifications(
                startDate: from,
                endDate: to
            ), cancellationToken);

            var result = consults
                .GroupBy(i => i.CreatedAt.Date) // Agrupar por día (sin la hora)
                .Select(g => new ReportConsultByDateDto
                {
                    Date = g.Key, // Día (fecha sin hora)
                    Count = g.Count() // Cantidad de consultas
                })
                .ToList();

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}