using DMSpro.OMS.MdmService.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SalesOrgHierarchies;
using Volo.Abp.Content;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.SalesOrgHierarchies
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesOrgHierarchy")]
    [Route("api/mdm-service/sales-org-hierarchies")]
    public class SalesOrgHierarchyController : AbpController, ISalesOrgHierarchiesAppService
    {
        private readonly ISalesOrgHierarchiesAppService _salesOrgHierarchiesAppService;

        public SalesOrgHierarchyController(ISalesOrgHierarchiesAppService salesOrgHierarchiesAppService)
        {
            _salesOrgHierarchiesAppService = salesOrgHierarchiesAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<SalesOrgHierarchyWithNavigationPropertiesDto>> GetListAsync(GetSalesOrgHierarchiesInput input)
        {
            return _salesOrgHierarchiesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<SalesOrgHierarchyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _salesOrgHierarchiesAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _salesOrgHierarchiesAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesOrgHierarchyDto> GetAsync(Guid id)
        {
            return _salesOrgHierarchiesAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-header-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHeaderLookupAsync(LookupRequestDto input)
        {
            return _salesOrgHierarchiesAppService.GetSalesOrgHeaderLookupAsync(input);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid?>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _salesOrgHierarchiesAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<SalesOrgHierarchyDto> CreateAsync(SalesOrgHierarchyCreateDto input)
        {
            return _salesOrgHierarchiesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SalesOrgHierarchyDto> UpdateAsync(Guid id, SalesOrgHierarchyUpdateDto input)
        {
            return _salesOrgHierarchiesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesOrgHierarchiesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHierarchyExcelDownloadDto input)
        {
            return _salesOrgHierarchiesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _salesOrgHierarchiesAppService.GetDownloadTokenAsync();
        }
    }
}