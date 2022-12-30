using DMSpro.OMS.MdmService.Holidays;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailWithNavigationPropertiesDto
    {
        public HolidayDetailDto HolidayDetail { get; set; }

        public HolidayDto Holiday { get; set; }

    }
}