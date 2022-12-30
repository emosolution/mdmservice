using DMSpro.OMS.MdmService.PriceLists;
using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactorMin { get; set; }
        public int? ArithmeticFactorMax { get; set; }
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool? IsFirstPriceList { get; set; }
        public Guid? BasePriceListId { get; set; }

        public PriceListExcelDownloadDto()
        {

        }
    }
}