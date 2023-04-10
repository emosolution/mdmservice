using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Items
{
    public partial interface IItemsAppService : IApplicationService
    {
        Task<ItemDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemDto> CreateAsync(ItemCreateDto input);

        Task<ItemDto> UpdateAsync(Guid id, ItemUpdateDto input);
    }
}