using Domain.Entities;
using MediatR;

namespace Application.Queries.Backup;

public record GetAllBackupQuery() : IRequest<List<BackupEntity>>;