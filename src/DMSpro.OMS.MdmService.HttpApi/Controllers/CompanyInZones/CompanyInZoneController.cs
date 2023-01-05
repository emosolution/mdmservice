using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.CompanyInZones;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.CompanyInZones
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("CompanyInZone")]
    [Route("api/mdm-service/company-in-zones")]
    public class CompanyInZoneController : AbpController, ICompanyInZonesAppService
    {
        private readonly ICompanyInZonesAppService _companyInZonesAppService;

        public CompanyInZoneController(ICompanyInZonesAppService companyInZonesAppService)
        {
            _companyInZonesAppService = companyInZonesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CompanyInZoneWithNavigationPropertiesDto>> GetListAsync(GetCompanyInZonesInput input)
        {
            return _companyInZonesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CompanyInZoneWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _companyInZonesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _companyInZonesAppService.GetListDevextremesAsync(inputDev);
        }
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyInZoneDto> GetAsync(Guid id)
        {
            return _companyInZonesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _companyInZonesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _companyInZonesAppService.GetCompanyLookupAsync(input);
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

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyInZoneExcelDownloadDto input)
        {
            return _companyInZonesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _companyInZonesAppService.GetDownloadTokenAsync();
        }
    }
}