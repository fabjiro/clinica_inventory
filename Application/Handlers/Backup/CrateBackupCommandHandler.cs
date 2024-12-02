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

namespace Application.Handlers.Backup;

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
            // get temp path
            var tempPath = Path.GetTempPath();
            var fileName = "database-backup-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".dump";
            var pathToSave = Path.Combine(tempPath, fileName);

            var resutl = await DatabaseHelper.BackupDatabase(_connectionString, pathToSave);

            if (resutl is false)
            {
                return Result<BackupResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "Backup not created",}
            });
            }

            var base64String = Convert.ToBase64String(File.ReadAllBytes(pathToSave));


            var upFile = await _uploaderRepository.Upload("data:application/dump;base64," + base64String);

            if (upFile is null)
            {
                return Result<BackupResDto>.Invalid(new List<ValidationError> {
                new () {ErrorMessage = "File not uploaded",}
            });
            }

            var backupEntity = await _backupRepository.AddAsync(new BackupEntity(
                fileName,
                upFile
            ), cancellationToken);

            File.Delete(pathToSave);

            return Result.Success(_mapper.Map<BackupResDto>(backupEntity));
        }
        catch (Exception ex)
        {
            return Result.Error(ErrorHelper.GetExceptionError(ex));
        }
    }
}
