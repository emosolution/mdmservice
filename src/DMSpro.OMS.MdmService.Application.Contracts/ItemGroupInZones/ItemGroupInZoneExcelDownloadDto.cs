using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemGroupInZones
{
    public class ItemGroupInZoneExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public bool? Active { get; set; }
        public string Description { get; set; }
        public Guid? SellingZoneId { get; set; }
        public Guid? ItemGroupId { get; set; }

        public ItemGroupInZoneExcelDownloadDto()
        {

        }
    }
}