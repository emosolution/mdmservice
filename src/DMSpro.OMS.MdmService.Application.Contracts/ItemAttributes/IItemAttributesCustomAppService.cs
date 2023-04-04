using DevExtreme.AspNet.Data.ResponseModel;
using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public partial interface IItemAttributesAppService 
    {
        Task<ItemAttributeDto> GetAsync(Guid id);

        Task<LoadResult> DeleteAsync();

        Task<LoadResult> CreateHierarchyAsync(ItemAttributeCreateDto input);

        Task<LoadResult> CreateFlatAsync(ItemAttributeCreateDto input);

        Task<LoadResult> UpdateAsync(Guid id, ItemAttributeUpdateDto input);

        Task<LoadResult> ResetAsync();
    }
}