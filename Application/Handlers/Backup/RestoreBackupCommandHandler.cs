using Application.Commands.Backup;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Handlers.Backup;

public class RestoreBackupCommandHandler : IRequestHandler<RestoreBackupCommand, Result<BackupEntity>>
{
    private readonly IAsyncRepository<BackupEntity> _backupRepository;
    private readonly string _connectionString;

    public RestoreBackupCommandHandler(IAsyncRepository<BackupEntity> backupRepository, IConfiguration configuration)
    {
        _backupRepository = backupRepository;
        _connectionString = configuration.GetConnectionString("Default") ?? "";
    }

    public async Task<Result<BackupEntity>> Handle(RestoreBackupCommand request, CancellationToken cancellationToken)
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

            // temp path
            var tempPath = Path.GetTempPath();
            var pathToSave = Path.Combine(tempPath, backup.Name);

            // download file
            var file = await FileDownloadHelper.DownloadFileAsync(backup.Url, pathToSave);

            if (file is false)
            {
                return Result<BackupEntity>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Backup not restored",}
            });
            }

            // restore
            await DatabaseHelper.RestoreDatabase(_connectionString, pathToSave);

            // delete file
            File.Delete(pathToSave);

            return Result.Success(backup);
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));

        }
    }
}