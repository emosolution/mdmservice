using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.UOMGroups;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.UOMGroups
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("UOMGroup")]
    [Route("api/mdm-service/u-oMGroups")]
    public class UOMGroupController : AbpController, IUOMGroupsAppService
    {
        private readonly IUOMGroupsAppService _uOMGroupsAppService;

        public UOMGroupController(IUOMGroupsAppService uOMGroupsAppService)
        {
            _uOMGroupsAppService = uOMGroupsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<UOMGroupDto>> GetListAsync(GetUOMGroupsInput input)
        {
            return _uOMGroupsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _uOMGroupsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<UOMGroupDto> GetAsync(Guid id)
        {
            return _uOMGroupsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<UOMGroupDto> CreateAsync(UOMGroupCreateDto input)
        {
            return _uOMGroupsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<UOMGroupDto> UpdateAsync(Guid id, UOMGroupUpdateDto input)
        {
            return _uOMGroupsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _uOMGroupsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(UOMGroupExcelDownloadDto input)
        {
            return _uOMGroupsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _uOMGroupsAppService.GetDownloadTokenAsync();
        }
    }
}