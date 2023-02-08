using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;

namespace DMSpro.OMS.MdmService.UOMGroupDetails
{
    public partial interface IUOMGroupDetailsAppService : IApplicationService, IPartialAppService
    {
        Task<PagedResultDto<UOMGroupDetailWithNavigationPropertiesDto>> GetListAsync(GetUOMGroupDetailsInput input);

        Task<UOMGroupDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<UOMGroupDetailDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetUOMLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<UOMGroupDetailDto> CreateAsync(UOMGroupDetailCreateDto input);

        Task<UOMGroupDetailDto> UpdateAsync(Guid id, UOMGroupDetailUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupDetailExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}