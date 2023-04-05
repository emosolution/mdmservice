using System;
using System.Threading.Tasks;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public partial interface ICustomerGroupGeosAppService 
    {
        Task<CustomerGroupGeoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input);

        Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input);
    }
}