using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Vendors;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.Controllers.Vendors
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Vendor")]
    [Route("api/mdm-service/vendors")]
    public partial class VendorController : AbpController, IVendorsAppService
    {
        private readonly IVendorsAppService _vendorsAppService;

        public VendorController(IVendorsAppService vendorsAppService)
        {
            _vendorsAppService = vendorsAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VendorDto> GetAsync(Guid id)
        {
            return _vendorsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<VendorDto> CreateAsync(VendorCreateDto input)
        {
            return _vendorsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VendorDto> UpdateAsync(Guid id, VendorUpdateDto input)
        {
            return _vendorsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vendorsAppService.DeleteAsync(id);
        }
    }
}