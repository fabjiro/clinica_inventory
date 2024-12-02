using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Backup;

public record RestoreBackupCommand(Guid Id) : IRequest<Result<BackupEntity>>;