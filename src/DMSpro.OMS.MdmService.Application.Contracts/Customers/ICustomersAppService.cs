using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Customers
{
    public partial interface ICustomersAppService 
    {
        Task<CustomerDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerDto> CreateAsync(CustomerCreateDto input);

        Task<CustomerDto> UpdateAsync(Guid id, CustomerUpdateDto input);
    }
}