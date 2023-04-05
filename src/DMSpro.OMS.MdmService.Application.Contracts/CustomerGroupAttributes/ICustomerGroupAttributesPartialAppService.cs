using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public partial interface ICustomerGroupAttributesAppService : IApplicationService
    {
        Task<CustomerGroupAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupAttributeDto> CreateAsync(CustomerGroupAttributeCreateDto input);

        Task<CustomerGroupAttributeDto> UpdateAsync(Guid id, CustomerGroupAttributeUpdateDto input);
    }
}