using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using DMSpro.OMS.MdmService.CompanyInZones;

namespace DMSpro.OMS.MdmService.Controllers.CompanyInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CompanyInZone")]
    [Route("api/mdm-service/company-in-zones")]
    public partial class CompanyInZoneController : AbpController, ICompanyInZonesAppService
    {
        private readonly ICompanyInZonesAppService _companyInZonesAppService;

        public CompanyInZoneController(ICompanyInZonesAppService companyInZonesAppService)
        {
            _companyInZonesAppService = companyInZonesAppService;
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyInZoneDto> GetAsync(Guid id)
        {
            return _companyInZonesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input)
        {
            return _companyInZonesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input)
        {
            return _companyInZonesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companyInZonesAppService.DeleteAsync(id);
        }
    }
}