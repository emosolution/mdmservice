using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public interface ICustomerGroupListsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupListsInput input);

        Task<CustomerGroupListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerGroupListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupListDto> CreateAsync(CustomerGroupListCreateDto input);

        Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupListExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}