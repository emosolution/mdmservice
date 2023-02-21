using System;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageExcelDto
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public Guid FileId { get; set; }
    }
}