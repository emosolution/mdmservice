using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerGroupByGeos
{
    public class GetCustomerGroupByGeosInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? Active { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public Guid? GeoMasterId { get; set; }

        public GetCustomerGroupByGeosInput()
        {

        }
    }
}