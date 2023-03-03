using DMSpro.OMS.MdmService.PriceLists;
using System;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListExcelDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactor { get; set; }
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool IsFirstPriceList { get; set; }
    }
}