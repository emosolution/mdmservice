using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyCreateRouteDto
    {
        [StringLength(SalesOrgHierarchyConsts.NameMaxLength)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}