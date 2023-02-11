using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerAttributes
{
    public partial interface ICustomerAttributesAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerAttributeDto>> GetListAsync(GetCustomerAttributesInput input);

        Task<CustomerAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerAttributeDto> CreateAsync(CustomerAttributeCreateDto input);

        Task<CustomerAttributeDto> UpdateAsync(Guid id, CustomerAttributeUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(CustomerAttributeExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}