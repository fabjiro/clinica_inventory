using Application.Dto.Response.Backup;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Backup;

public record CreateBackupCommand() : IRequest<Result<BackupResDto>>;