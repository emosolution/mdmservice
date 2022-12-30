using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.PricelistAssignments
{
    public class GetPricelistAssignmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? CustomerGroupId { get; set; }

        public GetPricelistAssignmentsInput()
        {

        }
    }
}