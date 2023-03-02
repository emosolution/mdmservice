using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderCreateDto
    {
        [Required]
        [StringLength(SalesOrgHeaderConsts.CodeMaxLength, MinimumLength = SalesOrgHeaderConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(SalesOrgHeaderConsts.NameMaxLength)]
        public string Name { get; set; }
        public bool Active { get; set; } = true;
    }
}