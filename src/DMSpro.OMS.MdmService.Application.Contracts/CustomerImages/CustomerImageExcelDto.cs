using System;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class CustomerImageExcelDto
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool IsAvatar { get; set; }
        public bool IsPOSM { get; set; }
        public Guid FileId { get; set; }
    }
}