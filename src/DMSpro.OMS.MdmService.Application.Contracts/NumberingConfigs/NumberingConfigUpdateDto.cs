using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigUpdateDto : IHasConcurrencyStamp
    {
        public int StartNumber { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Length { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? SystemDataId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}