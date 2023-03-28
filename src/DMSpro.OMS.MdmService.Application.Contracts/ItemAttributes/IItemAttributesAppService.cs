using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public partial interface IItemAttributesAppService 
    {
        Task<ItemAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input);

        Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input);
    }
}