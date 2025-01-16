using Application.Dto.Response.Report;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Report;

public record ReportTopPatientQuery(
    int Top = 5
) : IRequest<Result<List<ReportTopPatientDto>>>;