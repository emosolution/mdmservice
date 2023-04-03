using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public partial interface ICustomerInZonesAppService : IApplicationService
    {
        Task<CustomerInZoneDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input);

        Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input);
    }
}