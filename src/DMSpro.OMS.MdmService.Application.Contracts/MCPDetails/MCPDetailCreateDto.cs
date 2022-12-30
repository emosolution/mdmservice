using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.MCPDetails
{
    public class MCPDetailCreateDto
    {
        [Required]
        [StringLength(MCPDetailConsts.CodeMaxLength, MinimumLength = MCPDetailConsts.CodeMinLength)]
        public string Code { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Distance { get; set; } = 0;
        public int VisitOrder { get; set; } = 0;
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Week1 { get; set; }
        public bool Week2 { get; set; }
        public bool Week3 { get; set; }
        public bool Week4 { get; set; }
        public Guid CustomerId { get; set; }
        public Guid MCPHeaderId { get; set; }
    }
}