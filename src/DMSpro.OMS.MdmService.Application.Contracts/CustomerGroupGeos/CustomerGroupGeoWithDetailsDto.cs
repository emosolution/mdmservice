using DMSpro.OMS.MdmService.CustomerGroups;
using DMSpro.OMS.MdmService.GeoMasters;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class CustomerGroupGeoWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public Guid CustomerGroupId { get; set; }
        public Guid GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CustomerGroupDto CustomerGroup { get; set; }
        public GeoMasterDto GeoMaster0 { get; set; }
        public GeoMasterDto GeoMaster1 { get; set; }
        public GeoMasterDto GeoMaster2 { get; set; }
        public GeoMasterDto GeoMaster3 { get; set; }
        public GeoMasterDto GeoMaster4 { get; set; }

        public CustomerGroupGeoWithDetailsDto()
        {

        }
    }
}