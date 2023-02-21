using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.PriceLists
{
	public class PriceListWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
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

        public PriceListDto BasePriceList { get; set; }
        public PriceListWithDetailsDto()
		{
		}
	}
}

