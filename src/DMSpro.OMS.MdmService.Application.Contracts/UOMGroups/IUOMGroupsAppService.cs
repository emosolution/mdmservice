using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.UOMGroups
{
    public interface IUOMGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<UOMGroupDto>> GetListAsync(GetUOMGroupsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<UOMGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<UOMGroupDto> CreateAsync(UOMGroupCreateDto input);

        Task<UOMGroupDto> UpdateAsync(Guid id, UOMGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}