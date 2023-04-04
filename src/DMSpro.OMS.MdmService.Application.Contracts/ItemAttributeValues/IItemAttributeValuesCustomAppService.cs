using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public partial interface IItemAttributeValuesAppService : IApplicationService
    {
        Task<ItemAttributeValueDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeValueDto> CreateAsync(ItemAttributeValueCreateDto input);

        Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input);
    }
}