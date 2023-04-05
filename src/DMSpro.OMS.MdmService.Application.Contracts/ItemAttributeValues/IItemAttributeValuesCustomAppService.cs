using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public partial interface IItemAttributeValuesAppService : IApplicationService
    {
        Task<ItemAttributeValueDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeValueDto> CreateHierarchyAsync(ItemAttributeValueCreateHierarchyDto input);
        
        Task<ItemAttributeValueDto> CreateRootAsync(ItemAttributeValueCreateRootDto input);

        Task<ItemAttributeValueDto> CreateFlatAsync(ItemAttributeValueCreateFlatDto input);

        Task<ItemAttributeValueDto> UpdateAsync(Guid id, ItemAttributeValueUpdateDto input);
    }
}