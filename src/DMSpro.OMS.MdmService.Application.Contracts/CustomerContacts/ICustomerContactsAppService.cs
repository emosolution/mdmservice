using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerContacts
{
    public partial interface ICustomerContactsAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerContactWithNavigationPropertiesDto>> GetListAsync(GetCustomerContactsInput input);

        Task<CustomerContactWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerContactDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerContactDto> CreateAsync(CustomerContactCreateDto input);

        Task<CustomerContactDto> UpdateAsync(Guid id, CustomerContactUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerContactExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}