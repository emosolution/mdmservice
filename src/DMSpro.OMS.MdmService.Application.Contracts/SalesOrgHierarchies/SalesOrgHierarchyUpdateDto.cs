using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHierarchies
{
    public class SalesOrgHierarchyUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(SalesOrgHierarchyConsts.NameMaxLength)]
        public string Name { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}