using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.VisitPlans;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.VisitPlans
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("VisitPlan")]
    [Route("api/mdm-service/visit-plans")]
    public class VisitPlanController : AbpController, IVisitPlansAppService
    {
        private readonly IVisitPlansAppService _visitPlansAppService;

        public VisitPlanController(IVisitPlansAppService visitPlansAppService)
        {
            _visitPlansAppService = visitPlansAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<VisitPlanWithNavigationPropertiesDto>> GetListAsync(GetVisitPlansInput input)
        {
            return _visitPlansAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _visitPlansAppService.GetListDevextremesAsync(inputDev);
        }
        
        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<VisitPlanWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _visitPlansAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VisitPlanDto> GetAsync(Guid id)
        {
            return _visitPlansAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("m-cPDetail-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetMCPDetailLookupAsync(LookupRequestDto input)
        {
            return _visitPlansAppService.GetMCPDetailLookupAsync(input);
        }

        [HttpGet]
        [Route("customer-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _visitPlansAppService.GetCustomerLookupAsync(input);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _visitPlansAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _visitPlansAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _visitPlansAppService.GetItemGroupLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<VisitPlanDto> CreateAsync(VisitPlanCreateDto input)
        {
            return _visitPlansAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VisitPlanDto> UpdateAsync(Guid id, VisitPlanUpdateDto input)
        {
            return _visitPlansAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _visitPlansAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VisitPlanExcelDownloadDto input)
        {
            return _visitPlansAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _visitPlansAppService.GetDownloadTokenAsync();
        }
    }
}