using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroupByLists
{
    public partial interface ICustomerGroupByListsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupByListWithNavigationPropertiesDto>> GetListAsync(GetCustomerGroupByListsInput input);

        Task<CustomerGroupByListWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerGroupByListDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupByListDto> CreateAsync(CustomerGroupByListCreateDto input);

        Task<CustomerGroupByListDto> UpdateAsync(Guid id, CustomerGroupByListUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerGroupByListExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}