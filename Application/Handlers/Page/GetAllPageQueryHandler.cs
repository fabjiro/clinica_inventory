using Application.Helpers;
using Application.Queries.Page;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Page;

public class GetAllPageQueryHandler : IRequestHandler<GetAllPageQuery, Result<List<PageEntity>>>
{
    private readonly IAsyncRepository<PageEntity> _repository;

    public GetAllPageQueryHandler(IAsyncRepository<PageEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<PageEntity>>> Handle(GetAllPageQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.ListAsync(cancellationToken);
            return Result.Success(result);
        }
        catch (System.Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}