using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.SalesOrgHeaders;

namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHeaders
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrgHeader")]
    [Route("api/mdm-service/sales-org-headers")]
    public partial class SalesOrgHeaderController : AbpController, ISalesOrgHeadersAppService
    {
        private readonly ISalesOrgHeadersAppService _salesOrgHeadersAppService;

        public SalesOrgHeaderController(ISalesOrgHeadersAppService salesOrgHeadersAppService)
        {
            _salesOrgHeadersAppService = salesOrgHeadersAppService;
        }
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesOrgHeaderDto> GetAsync(Guid id)
        {
            return _salesOrgHeadersAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SalesOrgHeaderDto> CreateAsync(SalesOrgHeaderCreateDto input)
        {
            return _salesOrgHeadersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("release/{id}")]
        public virtual Task<SalesOrgHeaderDto> ReleaseAsync(Guid id)
        {
            return _salesOrgHeadersAppService.ReleaseAsync(id);
        }

        [HttpPut]
        [Route("inactive/{id}")]
        public virtual Task<SalesOrgHeaderDto> InactiveAsync(Guid id)
        {
            return _salesOrgHeadersAppService.InactiveAsync(id);
        }
    }
}