using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class SalesOrgHeaderUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(SalesOrgHeaderConsts.CodeMaxLength, MinimumLength = SalesOrgHeaderConsts.CodeMinLength)]
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}