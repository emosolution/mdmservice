using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SystemDatas;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.SystemDatas
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SystemData")]
    [Route("api/mdm-service/system-datas")]
    public class SystemDataController : AbpController, ISystemDatasAppService
    {
        private readonly ISystemDatasAppService _systemDatasAppService;

        public SystemDataController(ISystemDatasAppService systemDatasAppService)
        {
            _systemDatasAppService = systemDatasAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SystemDataDto>> GetListAsync(GetSystemDatasInput input)
        {
            return _systemDatasAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _systemDatasAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SystemDataDto> GetAsync(Guid id)
        {
            return _systemDatasAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SystemDataDto> CreateAsync(SystemDataCreateDto input)
        {
            return _systemDatasAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SystemDataDto> UpdateAsync(Guid id, SystemDataUpdateDto input)
        {
            return _systemDatasAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _systemDatasAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SystemDataExcelDownloadDto input)
        {
            return _systemDatasAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _systemDatasAppService.GetDownloadTokenAsync();
        }
    }
}