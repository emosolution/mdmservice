using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Prefix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Suffix { get; set; }
        public bool Active { get; set; }
        public int CurrentNumber { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}