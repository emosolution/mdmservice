using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerGroups
{
    public partial interface ICustomerGroupsAppService : IApplicationService
    {
        Task<CustomerGroupDto> GetAsync(Guid id);

        Task ReleaseAsync(Guid id);

        Task<CustomerGroupDto> CreateAsync(CustomerGroupCreateDto input);

        Task<CustomerGroupDto> UpdateAsync(Guid id, CustomerGroupUpdateDto input);
    }
}