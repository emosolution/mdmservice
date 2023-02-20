using System;
using DMSpro.OMS.MdmService.CustomerAttributes;
using static DMSpro.OMS.MdmService.Permissions.MdmServicePermissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
namespace DMSpro.OMS.MdmService.CusAttributeValues
{
	public class CusAttributeValueWithDetailsDto: FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
	{
        public string AttrValName { get; set; }
        public Guid CustomerAttributeId { get; set; }
        public Guid? ParentCusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
        public CusAttributeValueDto Parent { get; set; }
        public CustomerAttributeDto CustomerAttribute { get; set; }
        public CusAttributeValueWithDetailsDto()
		{
		}
	}
}

