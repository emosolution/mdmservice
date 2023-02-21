using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentCreateDto
    {
        public string Description { get; set; }
        public bool Active { get; set; } = true;
        public Guid FileId { get; set; }
        public Guid EmployeeProfileId { get; set; }
    }
}