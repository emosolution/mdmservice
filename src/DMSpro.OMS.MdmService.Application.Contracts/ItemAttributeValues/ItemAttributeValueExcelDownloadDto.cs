using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemAttributeValues
{
    public class ItemAttributeValueExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string AttrValName { get; set; }
        public Guid? ItemAttributeId { get; set; }
        public Guid? ParentId { get; set; }

        public ItemAttributeValueExcelDownloadDto()
        {

        }
    }
}