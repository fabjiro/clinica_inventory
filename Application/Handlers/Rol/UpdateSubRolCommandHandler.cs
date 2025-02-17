using Application.Commands.Rol;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Rol;

public class UpdateSubRolCommandHandler : IRequestHandler<UpdateSubRolCommand, Result<Guid>>
{
    private readonly IAsyncRepository<SubRolEntity> _subRolRepository;

    public UpdateSubRolCommandHandler(IAsyncRepository<SubRolEntity> subRolRepository)
    {
        _subRolRepository = subRolRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateSubRolCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var subRol = await _subRolRepository.GetByIdAsync(request.Id, cancellationToken);

            if (subRol == null)
            {
                return Result.Invalid(new List<ValidationError>
                {
                    new () {ErrorMessage = "SubRol not found",}
                });
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                subRol.Name = request.Name;
            }

            if (request.RolId.HasValue)
            {
                subRol.RolId = request.RolId.Value;
            }

            await _subRolRepository.UpdateAsync(subRol, cancellationToken);

            return Result.Success(subRol.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}