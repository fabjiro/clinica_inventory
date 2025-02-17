using Application.Commands.Rol;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Handlers.Rol;

public class AddSubRolCommadHandler : IRequestHandler<AddSubRolCommad, Result<Guid>>
{
    private readonly IAsyncRepository<SubRolEntity> _subRolRepository;

    private readonly IMapper _mapper;

    public AddSubRolCommadHandler(IAsyncRepository<SubRolEntity> subRolRepository, IMapper mapper)
    {
        _subRolRepository = subRolRepository;
        _mapper = mapper;
    }

    
    public async Task<Result<Guid>> Handle(AddSubRolCommad request, CancellationToken cancellationToken)
    {

        try
        {
            var subRol = new SubRolEntity(
                request.Name,
                request.RolId
            );

            await _subRolRepository.AddAsync(subRol, cancellationToken);

            return Result.Success(subRol.Id);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
