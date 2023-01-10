using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.VATs;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.VATs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("VAT")]
    [Route("api/mdm-service/v-aTs")]
    public partial class VATController : AbpController, IVATsAppService
    {
        private readonly IVATsAppService _vATsAppService;

        public VATController(IVATsAppService vATsAppService)
        {
            _vATsAppService = vATsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<VATDto>> GetListAsync(GetVATsInput input)
        {
            return _vATsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<VATDto> GetAsync(Guid id)
        {
            return _vATsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<VATDto> CreateAsync(VATCreateDto input)
        {
            return _vATsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<VATDto> UpdateAsync(Guid id, VATUpdateDto input)
        {
            return _vATsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _vATsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(VATExcelDownloadDto input)
        {
            return _vATsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _vATsAppService.GetDownloadTokenAsync();
        }
    }
}