using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListCreateDto
    {
        [Required]
        [StringLength(PriceListConsts.CodeMaxLength, MinimumLength = PriceListConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(PriceListConsts.NameMaxLength)]
        public string Name { get; set; }
        public bool Active { get; set; } = false;
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactor { get; set; }
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool IsDefault { get; set; } = false;
        public Guid? BasePriceListId { get; set; }
    }
}