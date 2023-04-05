using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public interface ICustomerAttributeValuesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAttributeValueWithNavigationPropertiesDto>> GetListAsync(GetCustomerAttributeValuesInput input);

        Task<CustomerAttributeValueWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<CustomerAttributeValueDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetCustomerAttributeValueLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input);

        Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeValueExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}