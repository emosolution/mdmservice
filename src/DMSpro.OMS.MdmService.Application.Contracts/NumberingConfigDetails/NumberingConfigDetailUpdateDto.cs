using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailUpdateDto : IHasConcurrencyStamp
    {
        public bool Active { get; set; }
        [StringLength(NumberingConfigDetailConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}