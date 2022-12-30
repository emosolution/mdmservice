using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerInZones
{
    public class GetCustomerInZonesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public Guid? SalesOrgHierarchyId { get; set; }
        public Guid? CustomerId { get; set; }

        public GetCustomerInZonesInput()
        {

        }
    }
}