using DMSpro.OMS.MdmService.Holidays;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.HolidayDetails
{
    public class HolidayDetailWithNavigationProperties
    {
        public HolidayDetail HolidayDetail { get; set; }

        public Holiday Holiday { get; set; }
        

        
    }
}