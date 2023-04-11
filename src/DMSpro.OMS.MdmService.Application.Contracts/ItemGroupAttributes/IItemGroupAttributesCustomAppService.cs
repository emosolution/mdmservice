using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemGroupAttributes
{
    public partial interface IItemGroupAttributesAppService : IApplicationService
    {
        Task<ItemGroupAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemGroupAttributeDto> CreateAsync(ItemGroupAttributeCreateDto input);

        Task<ItemGroupAttributeDto> UpdateAsync(Guid id, ItemGroupAttributeUpdateDto input);
    }
}