using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;
using DMSpro.OMS.MdmService.Partial;

namespace DMSpro.OMS.MdmService.VATs
{
    public partial interface IVATsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<VATDto>> GetListAsync(GetVATsInput input);

        Task<VATDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<VATDto> CreateAsync(VATCreateDto input);

        Task<VATDto> UpdateAsync(Guid id, VATUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(VATExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}