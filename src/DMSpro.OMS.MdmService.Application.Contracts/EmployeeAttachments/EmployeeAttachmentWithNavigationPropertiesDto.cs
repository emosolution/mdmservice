using DMSpro.OMS.MdmService.EmployeeProfiles;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentWithNavigationPropertiesDto
    {
        public EmployeeAttachmentDto EmployeeAttachment { get; set; }

        public EmployeeProfileDto EmployeeProfile { get; set; }

    }
}