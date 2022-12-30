using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupByAtts
{
    public class CustomerGroupByAttDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string ValueCode { get; set; }
        public string ValueName { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid CusAttributeValueId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}