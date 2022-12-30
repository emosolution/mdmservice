using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Streets;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.Streets
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Street")]
    [Route("api/mdm-service/streets")]
    public class StreetController : AbpController, IStreetsAppService
    {
        private readonly IStreetsAppService _streetsAppService;

        public StreetController(IStreetsAppService streetsAppService)
        {
            _streetsAppService = streetsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<StreetDto>> GetListAsync(GetStreetsInput input)
        {
            return _streetsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StreetDto> GetAsync(Guid id)
        {
            return _streetsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<StreetDto> CreateAsync(StreetCreateDto input)
        {
            return _streetsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StreetDto> UpdateAsync(Guid id, StreetUpdateDto input)
        {
            return _streetsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _streetsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(StreetExcelDownloadDto input)
        {
            return _streetsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _streetsAppService.GetDownloadTokenAsync();
        }
    }
}