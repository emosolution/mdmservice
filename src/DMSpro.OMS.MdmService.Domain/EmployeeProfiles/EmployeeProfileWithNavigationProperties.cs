using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.SystemDatas;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileWithNavigationProperties
    {
        public EmployeeProfile EmployeeProfile { get; set; }

        public WorkingPosition WorkingPosition { get; set; }
        public SystemData SystemData { get; set; }
        

        
    }
}