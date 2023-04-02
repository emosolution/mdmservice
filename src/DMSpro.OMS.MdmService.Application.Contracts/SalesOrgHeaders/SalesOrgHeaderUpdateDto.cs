using DMSpro.OMS.MdmService.SalesOrgHeaders;
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
        [StringLength(SalesOrgHeaderConsts.NameMaxLength)]
        public string Name { get; set; }
        public bool Active { get; set; }
        public Status Status { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}