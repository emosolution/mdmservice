using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public interface ISalesOrgHeadersAppService : IApplicationService
    {
        Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<SalesOrgHeaderDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input);

        Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}