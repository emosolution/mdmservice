using DMSpro.OMS.MdmService.EmployeeAttachments;
using DMSpro.OMS.MdmService.EmployeeImages;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeProfiles
{
    public class EmployeeProfileFullDto
    {
        public EmployeeProfileDto Employee { get; set; }
        public List<EmployeeAttachmentDto> Attachments { get; set; }
        public List<EmployeeImageDto> Images { get; set; }
    }
}
