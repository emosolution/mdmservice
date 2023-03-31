using DMSpro.OMS.MdmService.Companies;
using DMSpro.OMS.MdmService.NumberingConfigs;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class NumberingConfigDetailWithDetailsDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Prefix { get; set; }
        public int PaddingZeroNumber { get; set; }
        public string Suffix { get; set; }
        public bool Active { get; set; }
        public int CurrentNumber { get; set; }
        public Guid NumberingConfigId { get; set; }
        public Guid CompanyId { get; set; }

        public string ConcurrencyStamp { get; set; }

        public CompanyDto Company { get; set; }
        public NumberingConfigDto NumberingConfig { get; set; }
        public NumberingConfigDetailWithDetailsDto()
        {
        }
    }
}