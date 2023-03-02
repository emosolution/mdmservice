using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(SalesOrgHierarchyConsts.CodeMaxLength, MinimumLength = SalesOrgHierarchyConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(SalesOrgHierarchyConsts.NameMaxLength)]
        public string Name { get; set; }
        [Range(SalesOrgHierarchyConsts.LevelMinLength, SalesOrgHierarchyConsts.LevelMaxLength)]
        public int Level { get; set; }
        public bool IsRoute { get; set; }
        public bool IsSellingZone { get; set; }
        [Required]
        [StringLength(SalesOrgHierarchyConsts.HierarchyCodeMaxLength, MinimumLength = SalesOrgHierarchyConsts.HierarchyCodeMinLength)]
        public string HierarchyCode { get; set; }
        public bool Active { get; set; }
        public Guid SalesOrgHeaderId { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}