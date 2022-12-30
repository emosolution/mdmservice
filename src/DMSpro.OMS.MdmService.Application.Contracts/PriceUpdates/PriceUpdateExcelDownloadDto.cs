using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public PriceUpdateStatus? Status { get; set; }
        public DateTime? UpdateStatusDateMin { get; set; }
        public DateTime? UpdateStatusDateMax { get; set; }
        public Guid? PriceListId { get; set; }

        public PriceUpdateExcelDownloadDto()
        {

        }
    }
}