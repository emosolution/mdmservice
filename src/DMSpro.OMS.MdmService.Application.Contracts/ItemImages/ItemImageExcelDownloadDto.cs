using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemImages
{
    public class ItemImageExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public int? DisplayOrderMin { get; set; }
        public int? DisplayOrderMax { get; set; }
        public Guid? FileId { get; set; }
        public Guid? ItemId { get; set; }

        public ItemImageExcelDownloadDto()
        {

        }
    }
}