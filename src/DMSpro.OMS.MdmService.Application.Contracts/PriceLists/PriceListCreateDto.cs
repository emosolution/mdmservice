using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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
        public bool IsDefaultForCustomer { get; set; } = false;
        public bool IsDefaultForVendor { get; set; } = false;
        public Guid? BasePriceListId { get; set; }
    }
}