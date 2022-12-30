using DMSpro.OMS.MdmService.WorkingPositions;
using DMSpro.OMS.MdmService.SystemDatas;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileWithNavigationPropertiesDto
    {
        public EmployeeProfileDto EmployeeProfile { get; set; }

        public WorkingPositionDto WorkingPosition { get; set; }
        public SystemDataDto SystemData { get; set; }

    }
}