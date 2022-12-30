using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.SystemDatas
{
    public interface ISystemDatasAppService : IApplicationService
    {
        Task<PagedResultDto<SystemDataDto>> GetListAsync(GetSystemDatasInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<SystemDataDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SystemDataDto> CreateAsync(SystemDataCreateDto input);

        Task<SystemDataDto> UpdateAsync(Guid id, SystemDataUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemDataExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}