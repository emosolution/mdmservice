using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.Vendors
{
    public partial interface IVendorsAppService : IApplicationService
    {
        Task<VendorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<VendorDto> CreateAsync(VendorCreateDto input);

        Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input);
    }
}