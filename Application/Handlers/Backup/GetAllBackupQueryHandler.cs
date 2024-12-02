using Application.Queries.Backup;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using MediatR;

namespace Application.Handlers.Backup;

public class GetAllBackupQueryHandler : IRequestHandler<GetAllBackupQuery, List<BackupEntity>>
{
    private readonly IAsyncRepository<BackupEntity> _backupRepository;

    public GetAllBackupQueryHandler(IAsyncRepository<BackupEntity> backupRepository)
    {
        _backupRepository = backupRepository;
    }
    public async Task<List<BackupEntity>> Handle(GetAllBackupQuery request, CancellationToken cancellationToken)
    {
        return await _backupRepository.ListAsync(cancellationToken);
    }
}
