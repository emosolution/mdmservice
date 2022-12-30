using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageWithNavigationPropertiesDto
    {
        public EmployeeImageDto EmployeeImage { get; set; }

        public EmployeeProfileDto EmployeeProfile { get; set; }

    }
}