using DMSpro.OMS.MdmService.Companies;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentWithNavigationPropertiesDto
    {
        public CompanyIdentityUserAssignmentDto CompanyIdentityUserAssignment { get; set; }

        public CompanyDto Company { get; set; }

    }
}