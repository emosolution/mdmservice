using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.UOMs;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.UOMs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("UOM")]
    [Route("api/mdm-service/u-oMs")]
    public partial class UOMController : AbpController, IUOMsAppService
    {
        private readonly IUOMsAppService _uOMsAppService;

        public UOMController(IUOMsAppService uOMsAppService)
        {
            _uOMsAppService = uOMsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<UOMDto>> GetListAsync(GetUOMsInput input)
        {
            return _uOMsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<UOMDto> GetAsync(Guid id)
        {
            return _uOMsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<UOMDto> CreateAsync(UOMCreateDto input)
        {
            return _uOMsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<UOMDto> UpdateAsync(Guid id, UOMUpdateDto input)
        {
            return _uOMsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _uOMsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMExcelDownloadDto input)
        {
            return _uOMsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _uOMsAppService.GetDownloadTokenAsync();
        }
    }
}