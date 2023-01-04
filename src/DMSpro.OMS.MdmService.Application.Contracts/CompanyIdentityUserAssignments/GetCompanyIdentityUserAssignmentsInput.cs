using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class GetCompanyIdentityUserAssignmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public Guid? IdentityUserId { get; set; }
        public Guid? CompanyId { get; set; }

        public GetCompanyIdentityUserAssignmentsInput()
        {

        }
    }
}