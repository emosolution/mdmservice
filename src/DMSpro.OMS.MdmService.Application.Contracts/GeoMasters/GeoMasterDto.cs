using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using DMSpro.OMS.MdmService.GeoMasters;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMasterDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string ERPCode { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public virtual GeoMasterDto Parent{get;set;}
    }
}