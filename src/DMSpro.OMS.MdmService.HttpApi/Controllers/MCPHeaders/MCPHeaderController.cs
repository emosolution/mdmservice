using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.MCPHeaders;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.MCPHeaders
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("MCPHeader")]
    [Route("api/mdm-service/m-cPHeaders")]
    public partial class MCPHeaderController : AbpController, IMCPHeadersAppService
    {
        private readonly IMCPHeadersAppService _mCPHeadersAppService;

        public MCPHeaderController(IMCPHeadersAppService mCPHeadersAppService)
        {
            _mCPHeadersAppService = mCPHeadersAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<MCPHeaderWithNavigationPropertiesDto>> GetListAsync(GetMCPHeadersInput input)
        {
            return _mCPHeadersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<MCPHeaderWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _mCPHeadersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<MCPHeaderDto> GetAsync(Guid id)
        {
            return _mCPHeadersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("sales-org-hierarchy-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetSalesOrgHierarchyLookupAsync(LookupRequestDto input)
        {
            return _mCPHeadersAppService.GetSalesOrgHierarchyLookupAsync(input);
        }

        [HttpGet]
        [Route("company-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCompanyLookupAsync(LookupRequestDto input)
        {
            return _mCPHeadersAppService.GetCompanyLookupAsync(input);
        }

        [HttpGet]
        [Route("item-group-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetItemGroupLookupAsync(LookupRequestDto input)
        {
            return _mCPHeadersAppService.GetItemGroupLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<MCPHeaderDto> CreateAsync(MCPHeaderCreateDto input)
        {
            return _mCPHeadersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<MCPHeaderDto> UpdateAsync(Guid id, MCPHeaderUpdateDto input)
        {
            return _mCPHeadersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _mCPHeadersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPHeaderExcelDownloadDto input)
        {
            return _mCPHeadersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _mCPHeadersAppService.GetDownloadTokenAsync();
        }
    }
}