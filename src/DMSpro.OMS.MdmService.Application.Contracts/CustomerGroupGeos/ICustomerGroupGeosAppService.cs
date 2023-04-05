using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public interface ICustomerGroupGeosAppService : IApplicationService
    {
        Task<CustomerGroupGeoDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input);

        Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input);
    }
}