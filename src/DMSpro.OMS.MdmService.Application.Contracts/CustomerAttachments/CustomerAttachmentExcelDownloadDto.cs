using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class CustomerAttachmentExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string url { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? CustomerId { get; set; }

        public CustomerAttachmentExcelDownloadDto()
        {

        }
    }
}