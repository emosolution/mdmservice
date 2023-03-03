using System;

namespace DMSpro.OMS.MdmService.PriceUpdates
{
    public class PriceUpdateExcelDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }
        public PriceUpdateStatus Status { get; set; }
        public DateTime? UpdateStatusDate { get; set; }
    }
}