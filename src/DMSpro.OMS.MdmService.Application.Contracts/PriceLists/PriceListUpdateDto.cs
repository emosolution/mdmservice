using DMSpro.OMS.MdmService.PriceLists;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceLists
{
    public class PriceListUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(PriceListConsts.CodeMaxLength, MinimumLength = PriceListConsts.CodeMinLength)]
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactor { get; set; }
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool IsFirstPriceList { get; set; }
        public Guid? BasePriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}