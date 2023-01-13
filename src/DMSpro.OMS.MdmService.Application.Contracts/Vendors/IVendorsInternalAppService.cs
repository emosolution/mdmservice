using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Vendors
{
    public interface IVendorsInternalAppService : IApplicationService
    {
        Task<VendorWithTenantDto> GetWithTenantIdAsynce(Guid id);
    }
}