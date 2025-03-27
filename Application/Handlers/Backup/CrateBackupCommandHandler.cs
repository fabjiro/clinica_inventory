using Application.Commands.Backup;
using Application.Dto.Response.Backup;
using Application.Helpers;
using Ardalis.Result;
using AutoMapper;
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
    public class CrateBackupCommandHandler : IRequestHandler<CreateBackupCommand, Result<BackupResDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<BackupEntity> _backupRepository;
        private readonly IUploaderRepository _uploaderRepository;
        private readonly string _connectionString;

        public CrateBackupCommandHandler(IMapper mapper, IAsyncRepository<BackupEntity> backupRepository, IUploaderRepository uploaderRepository, IConfiguration configuration)
        {
            _mapper = mapper;
            _backupRepository = backupRepository;
            _uploaderRepository = uploaderRepository;
            _connectionString = configuration.GetConnectionString("Default") ?? "";
        }

        public async Task<Result<BackupResDto>> Handle(CreateBackupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Ruta de la carpeta C:\backups
                var backupsPath = Path.Combine("C:", "backups");

                // Verificar si la carpeta "backups" existe, si no, crearla
                if (!Directory.Exists(backupsPath))
                {
                    Directory.CreateDirectory(backupsPath);
                }

                var fileName = "database-backup-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dump";
                var pathToSave = Path.Combine(backupsPath, fileName);

                // Generar el backup de la base de datos
                var result = await DatabaseHelper.BackupDatabase(_connectionString, pathToSave);

                if (!result)
                {
                    return Result<BackupResDto>.Invalid(new List<ValidationError> {
                        new ValidationError { ErrorMessage = "Backup not created" }
                    });
                }

                // Aquí ya no necesitamos subir el archivo a un servidor remoto
                // Así que no hace falta convertir a base64 ni subirlo

                // Guardar la información del backup en la base de datos
                var backupEntity = await _backupRepository.AddAsync(new BackupEntity(fileName, pathToSave), cancellationToken);

                // Devolver el resultado del backup
                return Result.Success(_mapper.Map<BackupResDto>(backupEntity));
            }
            catch (Exception ex)
            {
                return Result.Error(ErrorHelper.GetExceptionError(ex));
            }
        }
    }
}
