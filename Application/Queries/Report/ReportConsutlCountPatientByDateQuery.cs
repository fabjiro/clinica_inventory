using Application.Dto.Response.Report;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Report;

    public record ReportConsultCountPatientByDateQuery : IRequest<Result<List<ReportConsultByDateDto>>> {};