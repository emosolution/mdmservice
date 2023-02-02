using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SystemConfigs;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.SystemConfigs
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SystemConfig")]
    [Route("api/mdm-service/system-configs")]
    public partial class SystemConfigController : AbpController, ISystemConfigsAppService
    {
        private readonly ISystemConfigsAppService _systemConfigsAppService;

        public SystemConfigController(ISystemConfigsAppService systemConfigsAppService)
        {
            _systemConfigsAppService = systemConfigsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SystemConfigDto>> GetListAsync(GetSystemConfigsInput input)
        {
            return _systemConfigsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SystemConfigDto> GetAsync(Guid id)
        {
            return _systemConfigsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SystemConfigDto> CreateAsync(SystemConfigCreateDto input)
        {
            return _systemConfigsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SystemConfigDto> UpdateAsync(Guid id, SystemConfigUpdateDto input)
        {
            return _systemConfigsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _systemConfigsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemConfigExcelDownloadDto input)
        {
            return _systemConfigsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _systemConfigsAppService.GetDownloadTokenAsync();
        }
    }
}