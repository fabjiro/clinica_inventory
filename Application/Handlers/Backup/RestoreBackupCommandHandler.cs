using Application.Commands.Backup;
using Application.Helpers;
using Ardalis.Result;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.Backup
{
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
                        new ValidationError { ErrorMessage = "Backup not found" }
                    });
                }

                // Ruta de la carpeta C:\backups donde se encuentra el archivo de backup
                var backupFolderPath = Path.Combine("C:", "backups");

                // Ruta completa al archivo de backup
                var pathToRestore = Path.Combine(backupFolderPath, backup.Name);

                // Verificar si el archivo existe en la carpeta de backups
                if (!File.Exists(pathToRestore))
                {
                    return Result<BackupEntity>.Invalid(new List<ValidationError> {
                        new ValidationError { ErrorMessage = "Backup file not found" }
                    });
                }

                // Restaurar la base de datos desde el archivo
                await DatabaseHelper.RestoreDatabase(_connectionString, pathToRestore);

                return Result.Success(backup);
            }
            catch (Exception ex)
            {
                return Result.Error(ErrorHelper.GetExceptionError(ex));
            }
        }
    }
}
