
using Application.Dto.Response.Inventory;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Inventory
{
    public class InventoryMapper : Profile
    {
        public InventoryMapper()
        {
            CreateMap<InventoryHistoryEntity, InventoryHistoryResponse>();

        }

    }
}