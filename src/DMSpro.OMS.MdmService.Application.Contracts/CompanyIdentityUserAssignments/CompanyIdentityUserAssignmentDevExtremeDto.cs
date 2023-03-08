using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.IdentityUsers;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentDevExtremeDto
    {
        public CompanyIdentityUserAssignmentDto CompanyIdentityUserAssignment { get; set; }

        public CompanyDto Company { get; set; }

        public IdentityUserDto IdentityUser { get; set; }

        public CompanyIdentityUserAssignmentDevExtremeDto() { }
    }
}