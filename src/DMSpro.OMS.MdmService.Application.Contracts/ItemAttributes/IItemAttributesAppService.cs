using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;

namespace DMSpro.OMS.MdmService.ItemAttributes
{
    public interface IItemAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemAttributeDto>> GetListAsync(GetItemAttributesInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemAttributeDto> CreateAsync(ItemAttributeCreateDto input);

        Task<ItemAttributeDto> UpdateAsync(Guid id, ItemAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}