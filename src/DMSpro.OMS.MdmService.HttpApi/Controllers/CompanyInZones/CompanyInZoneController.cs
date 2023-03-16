using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CompanyInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

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
        public virtual async Task<CompanyInZoneDto> GetAsync(Guid id)
        {
            try
            {
                return await _companyInZonesAppService.GetAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPost]
        public virtual async Task<CompanyInZoneDto> CreateAsync(CompanyInZoneCreateDto input)
        {
            try
            {
                return await _companyInZonesAppService.CreateAsync(input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<CompanyInZoneDto> UpdateAsync(Guid id, CompanyInZoneUpdateDto input)
        {
            try
            {
                return await _companyInZonesAppService.UpdateAsync(id, input);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task DeleteAsync(Guid id)
        {
            try
            {
                await _companyInZonesAppService.DeleteAsync(id);
            }
            catch (BusinessException bex)
            {
                throw new UserFriendlyException(message: bex.Message, code: bex.Code, details: bex.Details);
            }
            catch (Exception e)
            {
                throw new UserFriendlyException(message: e.Message, code: "1");
            }
        }
    }
}