using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentCreateDto
    {
        public Guid IdentityUserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}