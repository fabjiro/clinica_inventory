using Application.Dto.Response.Report;
using Ardalis.Result;
using MediatR;

namespace Application.Querys.Report;

public record ReportMasterQuery(
    DateTime? StartDate = null,
    DateTime? EndDate = null
) : IRequest<Result<List<ReportMasterResDto>>>;