using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerGroupLists
{
    public interface ICustomerGroupListsAppService : IApplicationService
    {
        Task<CustomerGroupListDto> GetAsync(Guid id);
        Task DeleteAsync(Guid id);

        Task<CustomerGroupListDto> CreateAsync(CustomerGroupListCreateDto input);

        Task<CustomerGroupListDto> UpdateAsync(Guid id, CustomerGroupListUpdateDto input);
    }
}