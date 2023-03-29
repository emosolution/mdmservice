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
        public ArithmeticOperator? ArithmeticOperation { get; set; }
        public int? ArithmeticFactor { get; set; }
        public ArithmeticFactorType? ArithmeticFactorType { get; set; }
        public bool IsBase { get; set; }
        public bool IsDefaultForCustomer { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public bool IsDefaultForVendor { get; set; }
        public Guid? BasePriceListId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public PriceListDto BasePriceList { get; set; }
        
        public PriceListWithDetailsDto()
		{
		}
	}
}

