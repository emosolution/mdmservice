using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemGroupLists
{
    public class ItemGroupListExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public int? RateMin { get; set; }
        public int? RateMax { get; set; }
        public Guid? ItemGroupId { get; set; }
        public Guid? ItemId { get; set; }
        public Guid? UOMId { get; set; }

        public ItemGroupListExcelDownloadDto()
        {

        }
    }
}