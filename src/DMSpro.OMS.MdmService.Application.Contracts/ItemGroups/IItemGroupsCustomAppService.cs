using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemGroups
{
    public partial interface IItemGroupsAppService : IApplicationService
    {
        Task<ItemGroupDto> GetAsync(Guid id);

        Task ReleaseAsync(Guid id);

        Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input);

        Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input);
    }
}