using System.Threading.Tasks;
using DMSpro.OMS.Shared.Domain.Devextreme;
using DevExtreme.AspNet.Data.ResponseModel;
using System;

namespace DMSpro.OMS.MdmService.Vendors
{
    public partial interface IVendorsInternalAppService
    {
        Task<VendorWithTenantDto> GetWithTenantIdAsynce(Guid id);
    }
}