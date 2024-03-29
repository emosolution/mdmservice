using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class NumberingConfigDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Description { get; set; }
        public Guid? SystemDataId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}