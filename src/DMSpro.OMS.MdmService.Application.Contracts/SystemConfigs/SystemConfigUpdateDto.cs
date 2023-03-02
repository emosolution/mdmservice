using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(SystemConfigConsts.CodeMaxLength, MinimumLength = SystemConfigConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(SystemConfigConsts.DescriptionMaxLength, MinimumLength = SystemConfigConsts.DescriptionMinLength)]
        public string Description { get; set; }
        [Required]
        [StringLength(SystemConfigConsts.ValueMaxLength, MinimumLength = SystemConfigConsts.ValueMinLength)]
        public string Value { get; set; }
        [Required]
        [StringLength(SystemConfigConsts.DefaultValueMaxLength, MinimumLength = SystemConfigConsts.DefaultValueMinLength)]
        public string DefaultValue { get; set; }
        public bool EditableByTenant { get; set; }
        public ControlType ControlType { get; set; }
        [StringLength(SystemConfigConsts.DataSourceMaxLength)]
        public string DataSource { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}