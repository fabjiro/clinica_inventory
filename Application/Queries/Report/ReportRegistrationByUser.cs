using Application.Dto.Response.Report;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Report;

public record ReportRegistrationByUser(
    string UserId,
    DateTime? StartDate = null,
    DateTime? EndDate = null
) : IRequest<Result<List<ReportRegistrationByUserResDto>>>;