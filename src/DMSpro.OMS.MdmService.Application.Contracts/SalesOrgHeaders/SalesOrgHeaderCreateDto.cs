using System.ComponentModel.DataAnnotations;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderCreateDto
    {
        [Required]
        [StringLength(SalesOrgHeaderConsts.CodeMaxLength, MinimumLength = SalesOrgHeaderConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(SalesOrgHeaderConsts.NameMaxLength)]
        public string Name { get; set; }
    }
}