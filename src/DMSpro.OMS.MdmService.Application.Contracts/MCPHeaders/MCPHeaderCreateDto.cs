using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class MCPHeaderCreateDto
    {
        [Required]
        [StringLength(MCPHeaderConsts.CodeMaxLength, MinimumLength = MCPHeaderConsts.CodeMinLength)]
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid RouteId { get; set; }
        public Guid CompanyId { get; set; }
    }
}