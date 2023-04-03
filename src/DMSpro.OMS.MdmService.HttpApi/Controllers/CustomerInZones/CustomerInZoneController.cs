using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CustomerInZones;

namespace DMSpro.OMS.MdmService.Controllers.CustomerInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CustomerInZone")]
    [Route("api/mdm-service/customer-in-zones")]
    public partial class CustomerInZoneController : AbpController, ICustomerInZonesAppService
    {
        private readonly ICustomerInZonesAppService _customerInZonesAppService;

        public CustomerInZoneController(ICustomerInZonesAppService customerInZonesAppService)
        {
            _customerInZonesAppService = customerInZonesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CustomerInZoneDto> GetAsync(Guid id)
        {
            return _customerInZonesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CustomerInZoneDto> CreateAsync(CustomerInZoneCreateDto input)
        {
            return _customerInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CustomerInZoneDto> UpdateAsync(Guid id, CustomerInZoneUpdateDto input)
        {
            return _customerInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _customerInZonesAppService.DeleteAsync(id);
        }
    }
}