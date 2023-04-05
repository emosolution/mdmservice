using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupGeos
{
    public class GetCustomerGroupGeosInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? GeoMaster0Id { get; set; }
        public Guid? GeoMaster1Id { get; set; }
        public Guid? GeoMaster2Id { get; set; }
        public Guid? GeoMaster3Id { get; set; }
        public Guid? GeoMaster4Id { get; set; }

        public GetCustomerGroupGeosInput()
        {

        }
    }
}