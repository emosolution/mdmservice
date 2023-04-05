using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.CustomerGroupAttributes
{
    public interface ICustomerGroupAttributesAppService : IApplicationService
    {
        Task<CustomerGroupAttributeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupAttributeDto> CreateAsync(CustomerGroupAttributeCreateDto input);

        Task<CustomerGroupAttributeDto> UpdateAsync(Guid id, CustomerGroupAttributeUpdateDto input);
    }
}