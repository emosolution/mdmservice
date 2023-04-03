using System;
using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyCreateRootDto
    {
        [StringLength(SalesOrgHierarchyConsts.NameMaxLength)]
        public string Name { get; set; }
        public Guid SalesOrgHeaderId { get; set; }
    }
}