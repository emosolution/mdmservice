using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SalesOrgHeaders;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

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
        public virtual Task<PagedResultDto<SalesOrgHeaderDto>> GetListAsync(GetSalesOrgHeadersInput input)
        {
            return _salesOrgHeadersAppService.GetListAsync(input);
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
        [Route("{id}")]
        public virtual Task<SalesOrgHeaderDto> UpdateAsync(Guid id, SalesOrgHeaderUpdateDto input)
        {
            return _salesOrgHeadersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesOrgHeadersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesOrgHeaderExcelDownloadDto input)
        {
            return _salesOrgHeadersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _salesOrgHeadersAppService.GetDownloadTokenAsync();
        }
    }
}