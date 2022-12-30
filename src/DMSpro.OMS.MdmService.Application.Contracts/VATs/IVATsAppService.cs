using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
namespace DMSpro.OMS.MdmService.VATs
{
    public interface IVATsAppService : IApplicationService
    {
        Task<PagedResultDto<VATDto>> GetListAsync(GetVATsInput input);

        Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev);

        Task<VATDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<VATDto> CreateAsync(VATCreateDto input);

        Task<VATDto> UpdateAsync(Guid id, VATUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VATExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}