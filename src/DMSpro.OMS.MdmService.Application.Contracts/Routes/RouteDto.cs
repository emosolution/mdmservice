using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.Routes
{
    public class RouteDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public bool CheckIn { get; set; }
        public bool CheckOut { get; set; }
        public bool GPSLock { get; set; }
        public bool OutRoute { get; set; }
        public Guid RouteTypeId { get; set; }
        public Guid ItemGroupId { get; set; }
        public Guid SalesOrgHierarchyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}