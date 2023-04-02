using DMSpro.OMS.MdmService.SalesOrgHeaders;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Status Status { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}