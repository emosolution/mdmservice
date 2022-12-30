using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.ItemGroups
{
    public interface IItemGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupDto>> GetListAsync(GetItemGroupsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<ItemGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ItemGroupDto> CreateAsync(ItemGroupCreateDto input);

        Task<ItemGroupDto> UpdateAsync(Guid id, ItemGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}