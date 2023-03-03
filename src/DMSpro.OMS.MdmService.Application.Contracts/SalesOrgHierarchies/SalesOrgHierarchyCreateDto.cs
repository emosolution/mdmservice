using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyCreateDto
    {
        [Required]
        [StringLength(SalesOrgHierarchyConsts.CodeMaxLength, MinimumLength = SalesOrgHierarchyConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(SalesOrgHierarchyConsts.NameMaxLength)]
        public string Name { get; set; }
        [Range(SalesOrgHierarchyConsts.LevelMinLength, SalesOrgHierarchyConsts.LevelMaxLength)]
        public int Level { get; set; }
        public bool IsRoute { get; set; } = false;
        public bool IsSellingZone { get; set; } = false;
        [Required]
        [StringLength(SalesOrgHierarchyConsts.HierarchyCodeMaxLength, MinimumLength = SalesOrgHierarchyConsts.HierarchyCodeMinLength)]
        public string HierarchyCode { get; set; }
        public bool Active { get; set; } = true;
        public Guid SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }
    }
}