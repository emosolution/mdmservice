using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.MCPDetails;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.MCPDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("MCPDetail")]
    [Route("api/mdm-service/m-cPDetails")]
    public partial class MCPDetailController : AbpController, IMCPDetailsAppService
    {
        private readonly IMCPDetailsAppService _mCPDetailsAppService;

        public MCPDetailController(IMCPDetailsAppService mCPDetailsAppService)
        {
            _mCPDetailsAppService = mCPDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<MCPDetailWithNavigationPropertiesDto>> GetListAsync(GetMCPDetailsInput input)
        {
            return _mCPDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<MCPDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _mCPDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<MCPDetailDto> GetAsync(Guid id)
        {
            return _mCPDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("customer-profile-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetCustomerLookupAsync(LookupRequestDto input)
        {
            return _mCPDetailsAppService.GetCustomerLookupAsync(input);
        }

        [HttpGet]
        [Route("m-cPHeader-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetMCPHeaderLookupAsync(LookupRequestDto input)
        {
            return _mCPDetailsAppService.GetMCPHeaderLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<MCPDetailDto> CreateAsync(MCPDetailCreateDto input)
        {
            return _mCPDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<MCPDetailDto> UpdateAsync(Guid id, MCPDetailUpdateDto input)
        {
            return _mCPDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _mCPDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(MCPDetailExcelDownloadDto input)
        {
            return _mCPDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _mCPDetailsAppService.GetDownloadTokenAsync();
        }
    }
}