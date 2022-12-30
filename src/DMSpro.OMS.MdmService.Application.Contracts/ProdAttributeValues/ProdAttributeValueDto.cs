using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.ProdAttributeValues
{
    public class ProdAttributeValueDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string AttrValName { get; set; }
        public Guid ProdAttributeId { get; set; }
        public Guid? ParentProdAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}