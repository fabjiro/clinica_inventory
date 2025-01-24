using Application.Dto.Response.Report;
using Ardalis.Result;
using MediatR;

namespace Application.Querys.Report;

public record ReportRecentDiagnosticsQuery(
    DateTime? StartDate = null,
    DateTime? EndDate = null
) : IRequest<Result<List<ReportRecentConsultResDto>>> {}