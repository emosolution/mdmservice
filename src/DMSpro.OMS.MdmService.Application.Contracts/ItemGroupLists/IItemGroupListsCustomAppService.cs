using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public partial interface IItemGroupListsAppService : IApplicationService
    {
        Task<ItemGroupListDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemGroupListDto> CreateAsync(ItemGroupListCreateDto input);

        Task<ItemGroupListDto> UpdateAsync(Guid id, ItemGroupListUpdateDto input);
    }
}