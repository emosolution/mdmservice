using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public partial interface ICustomerGroupListsAppService : IApplicationService
    {
        Task<CustomerGroupListDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);

        Task<CustomerGroupListDto> CreateAsync(CustomerGroupListCreateDto input);

        Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input);
    }
}