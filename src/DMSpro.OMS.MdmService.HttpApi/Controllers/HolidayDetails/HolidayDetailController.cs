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
        [Route("{id}")]
        public virtual Task<HolidayDetailDto> GetAsync(Guid id)
        {
            return _holidayDetailsAppService.GetAsync(id);
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
    }
}