using Application.Commands.Backup;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Backup;

public class DeleteBackupCommandHandler : IRequestHandler<DeleteBackupCommand, Result<BackupEntity>>
{
    private readonly IAsyncRepository<BackupEntity> _backupRepository;
    private readonly IUploaderRepository _uploaderRepository;

    public DeleteBackupCommandHandler(IAsyncRepository<BackupEntity> backupRepository, IUploaderRepository uploaderRepository)
    {
        _backupRepository = backupRepository;
        _uploaderRepository = uploaderRepository;
    }

    public async Task<Result<BackupEntity>> Handle(DeleteBackupCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var backup = await _backupRepository.GetByIdAsync(request.Id, cancellationToken);

            if (backup is null)
            {
                return Result<BackupEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Backup not found",}
            });
            }

            await _backupRepository.DeleteAsync(backup, cancellationToken);
            await _uploaderRepository.Delete(backup.Url);
            return Result.Success(backup);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));

        }
    }
}
