using System;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentExcelDto
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid FileId { get; set; }
    }
}