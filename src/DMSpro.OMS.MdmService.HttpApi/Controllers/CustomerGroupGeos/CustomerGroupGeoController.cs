using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerGroupGeo")]
    [Route("api/mdm-service/customer-group-geos")]
    public partial class CustomerGroupGeoController : AbpController, ICustomerGroupGeosAppService
    {
        private readonly ICustomerGroupGeosAppService _customerGroupGeosAppService;

        public CustomerGroupGeoController(ICustomerGroupGeosAppService customerGroupGeosAppService)
        {
            _customerGroupGeosAppService = customerGroupGeosAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerGroupGeoDto> GetAsync(Guid id)
        {
            return _customerGroupGeosAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerGroupGeoDto> CreateAsync(CustomerGroupGeoCreateDto input)
        {
            return _customerGroupGeosAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerGroupGeoDto> UpdateAsync(Guid id, CustomerGroupGeoUpdateDto input)
        {
            return _customerGroupGeosAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerGroupGeosAppService.DeleteAsync(id);
        }
    }
}