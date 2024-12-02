using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Backup;

public record DeleteBackupCommand(Guid Id) : IRequest<Result<BackupEntity>>;