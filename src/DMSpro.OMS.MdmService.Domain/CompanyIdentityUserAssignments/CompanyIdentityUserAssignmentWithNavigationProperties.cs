using DMSpro.OMS.MdmService.Companies;

using System;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentWithNavigationProperties
    {
        public CompanyIdentityUserAssignment CompanyIdentityUserAssignment { get; set; }

        public Company Company { get; set; }
        

        
    }
}