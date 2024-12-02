using Application.Dto.Response.Backup;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Backup;

public class BackupMapper : Profile
{
    public BackupMapper()
    {
        CreateMap<BackupEntity, BackupResDto>();
    }
}