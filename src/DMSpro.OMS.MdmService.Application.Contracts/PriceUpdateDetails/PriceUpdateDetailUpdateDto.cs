using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceUpdateDetails
{
    public class PriceUpdateDetailUpdateDto : IHasConcurrencyStamp
    {
        public decimal NewPrice { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}