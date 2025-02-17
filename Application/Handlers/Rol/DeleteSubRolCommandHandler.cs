using Application.Commands.Rol;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Rol;

public class DeleteSubRolCommandHandler : IRequestHandler<DeleteSubRolCommand, Result<Guid>>
{
    private readonly IAsyncRepository<SubRolEntity> _subRolRepository;

    public DeleteSubRolCommandHandler(IAsyncRepository<SubRolEntity> subRolRepository)
    {
        _subRolRepository = subRolRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteSubRolCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var subRol = await _subRolRepository.GetByIdAsync(request.Id, cancellationToken);

            if (subRol is null)
            {
                return Result<Guid>.Invalid(new List<ValidationError> {
                    new () {ErrorMessage = "SubRol not found",}
                });
            }

            await _subRolRepository.DeleteAsync(subRol, cancellationToken);

            return Result.Success(subRol.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }

    }
}