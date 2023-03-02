using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string DefaultValue { get; set; }
        public bool EditableByTenant { get; set; }
        public ControlType ControlType { get; set; }
        public string DataSource { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}