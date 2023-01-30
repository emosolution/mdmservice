using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Customers
{
    public interface ICustomersInternalAppService : IApplicationService
    {
        Task<CustomerWithTenantDto> GetWithTenantIdAsynce(Guid id);
    }
}
