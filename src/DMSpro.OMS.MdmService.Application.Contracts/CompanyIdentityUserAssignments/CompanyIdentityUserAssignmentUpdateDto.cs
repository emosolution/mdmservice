using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.CompanyIdentityUserAssignments
{
    public class CompanyIdentityUserAssignmentUpdateDto : IHasConcurrencyStamp
    {
        public Guid IdentityUserId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}