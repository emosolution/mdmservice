using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using DMSpro.OMS.MdmService.HolidayDetails;
using Volo.Abp.Content;
using DMSpro.OMS.MdmService.Shared;

namespace DMSpro.OMS.MdmService.Controllers.HolidayDetails
{
    [RemoteService(Name = "MdmService")]
    [Area("mdmService")]
    [ControllerName("HolidayDetail")]
    [Route("api/mdm-service/holiday-details")]
    public partial class HolidayDetailController : AbpController, IHolidayDetailsAppService
    {
        private readonly IHolidayDetailsAppService _holidayDetailsAppService;

        public HolidayDetailController(IHolidayDetailsAppService holidayDetailsAppService)
        {
            _holidayDetailsAppService = holidayDetailsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<HolidayDetailWithNavigationPropertiesDto>> GetListAsync(GetHolidayDetailsInput input)
        {
            return _holidayDetailsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<HolidayDetailWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _holidayDetailsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<HolidayDetailDto> GetAsync(Guid id)
        {
            return _holidayDetailsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("holiday-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetHolidayLookupAsync(LookupRequestDto input)
        {
            return _holidayDetailsAppService.GetHolidayLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<HolidayDetailDto> CreateAsync(HolidayDetailCreateDto input)
        {
            return _holidayDetailsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<HolidayDetailDto> UpdateAsync(Guid id, HolidayDetailUpdateDto input)
        {
            return _holidayDetailsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _holidayDetailsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(HolidayDetailExcelDownloadDto input)
        {
            return _holidayDetailsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _holidayDetailsAppService.GetDownloadTokenAsync();
        }
    }
}