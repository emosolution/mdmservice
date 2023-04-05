using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerAttributeValues
{
    public interface ICustomerAttributeValuesAppService : IApplicationService
    {
        Task<CustomerAttributeValueDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerAttributeValueDto> CreateAsync(CustomerAttributeValueCreateDto input);

        Task<CustomerAttributeValueDto> UpdateAsync(Guid id, CustomerAttributeValueUpdateDto input);
    }
}