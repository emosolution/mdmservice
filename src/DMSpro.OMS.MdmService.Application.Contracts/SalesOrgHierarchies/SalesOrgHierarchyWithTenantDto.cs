using System;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyWithTenantDto : SalesOrgHierarchyDto
    {
        public Guid? TenantId { get; set; }
    }
}
