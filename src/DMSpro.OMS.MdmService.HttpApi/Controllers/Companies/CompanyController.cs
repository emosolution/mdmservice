using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Companies;
using Volo.Abp.Content;
using Microsoft.AspNetCore.Http;

namespace DMSpro.OMS.MdmService.Controllers.Companies
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Company")]
    [Route("api/mdm-service/companies")]
    public partial class CompanyController : AbpController, ICompaniesAppService
    {
        private readonly ICompaniesAppService _companiesAppService;

        public CompanyController(ICompaniesAppService companiesAppService)
        {
            _companiesAppService = companiesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input)
        {
            return _companiesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _companiesAppService.GetWithNavigationPropertiesAsync(id);
        }
        
        [HttpGet]
        [Route("{id}")]
        public virtual Task<CompanyDto> GetAsync(Guid id)
        {
            return _companiesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _companiesAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("geo-master-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetGeoMasterLookupAsync(LookupRequestDto input)
        {
            return _companiesAppService.GetGeoMasterLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {
            return _companiesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {
            return _companiesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _companiesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(CompanyExcelDownloadDto input)
        {
            return _companiesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _companiesAppService.GetDownloadTokenAsync();
        }
    }
}