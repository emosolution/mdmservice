using System;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class EmployeeAttachmentExcelDto
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
    }
}