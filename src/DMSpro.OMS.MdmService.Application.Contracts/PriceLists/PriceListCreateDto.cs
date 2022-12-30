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
        public string Name { get; set; }
        public bool Active { get; set; } = false;
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactor { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool IsFirstPriceList { get; set; } = false;
        public Guid? BasePriceListId { get; set; }
    }
}