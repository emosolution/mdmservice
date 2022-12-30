using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class ItemAttachmentExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public string URL { get; set; }
        public Guid? ItemId { get; set; }

        public ItemAttachmentExcelDownloadDto()
        {

        }
    }
}