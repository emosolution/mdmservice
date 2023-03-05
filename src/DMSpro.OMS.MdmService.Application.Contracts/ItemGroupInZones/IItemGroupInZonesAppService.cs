using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public partial interface IItemGroupInZonesAppService : IApplicationService
    {
        Task<PagedResultDto<ItemGroupInZoneWithNavigationPropertiesDto>> GetListAsync(GetItemGroupInZonesInput input);

        Task<ItemGroupInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<ItemGroupInZoneDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<ItemGroupInZoneDto> CreateAsync(ItemGroupInZoneCreateDto input);

        Task<ItemGroupInZoneDto> UpdateAsync(Guid id, ItemGroupInZoneUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(ItemGroupInZoneExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}