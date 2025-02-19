using Application.Commands.Page;
using Application.Helpers;
using Application.Specifications.Page;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Page;

public class TogglePagePermitCommandHandler : IRequestHandler<TogglePagePermitCommand, Result<bool>>
{
    private readonly IAsyncRepository<PagePermitsEntity> _pagePermitsRepository;

    public TogglePagePermitCommandHandler(IAsyncRepository<PagePermitsEntity> pagePermitsRepository)
    {
        _pagePermitsRepository = pagePermitsRepository;
    }

    public async Task<Result<bool>> Handle(TogglePagePermitCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var pagePermit = await _pagePermitsRepository.FirstOrDefaultAsync(
                new GetPagePermitSpecifications(request.PageId, request.RolId),
                cancellationToken
            );

            if (pagePermit is not null)
            {
                await _pagePermitsRepository.DeleteAsync(pagePermit, cancellationToken);
                return Result.Success(true);
            }

            await _pagePermitsRepository.AddAsync(new PagePermitsEntity
            {
                PageId = request.PageId,
                SubRolId = request.RolId
            }, cancellationToken);

            return Result.Success(true);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }

    }
}