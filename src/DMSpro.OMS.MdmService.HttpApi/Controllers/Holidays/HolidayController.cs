using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.Holidays;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.Holidays
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("Holiday")]
    [Route("api/mdm-service/holidays")]
    public partial class HolidayController : AbpController, IHolidaysAppService
    {
        private readonly IHolidaysAppService _holidaysAppService;

        public HolidayController(IHolidaysAppService holidaysAppService)
        {
            _holidaysAppService = holidaysAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<HolidayDto>> GetListAsync(GetHolidaysInput input)
        {
            return _holidaysAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<HolidayDto> GetAsync(Guid id)
        {
            return _holidaysAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<HolidayDto> CreateAsync(HolidayCreateDto input)
        {
            return _holidaysAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input)
        {
            return _holidaysAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _holidaysAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayExcelDownloadDto input)
        {
            return _holidaysAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _holidaysAppService.GetDownloadTokenAsync();
        }
    }
}