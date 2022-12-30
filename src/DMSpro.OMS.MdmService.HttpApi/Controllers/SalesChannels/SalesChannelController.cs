using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.SalesChannels;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

//Dev
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Data;
using DMSpro.OMS.Shared.Domain.Devextreme;
namespace DMSpro.OMS.MdmService.Controllers.SalesChannels
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("SalesChannel")]
    [Route("api/mdm-service/sales-channels")]
    public class SalesChannelController : AbpController, ISalesChannelsAppService
    {
        private readonly ISalesChannelsAppService _salesChannelsAppService;

        public SalesChannelController(ISalesChannelsAppService salesChannelsAppService)
        {
            _salesChannelsAppService = salesChannelsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<SalesChannelDto>> GetListAsync(GetSalesChannelsInput input)
        {
            return _salesChannelsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("GetListDevextremes")]
        public Task<LoadResult> GetListDevextremesAsync(DataLoadOptionDevextreme inputDev)
        {
            return _salesChannelsAppService.GetListDevextremesAsync(inputDev);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<SalesChannelDto> GetAsync(Guid id)
        {
            return _salesChannelsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<SalesChannelDto> CreateAsync(SalesChannelCreateDto input)
        {
            return _salesChannelsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<SalesChannelDto> UpdateAsync(Guid id, SalesChannelUpdateDto input)
        {
            return _salesChannelsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _salesChannelsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(SalesChannelExcelDownloadDto input)
        {
            return _salesChannelsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _salesChannelsAppService.GetDownloadTokenAsync();
        }
    }
}