using DMSpro.OMS.MdmService.SystemConfigs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SystemConfigs
{
    public class SystemConfigCreateDto
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
        public bool EditableByTenant { get; set; } = true;
        public ControlType ControlType { get; set; } = ((ControlType[])Enum.GetValues(typeof(ControlType)))[0];
        [StringLength(SystemConfigConsts.DataSourceMaxLength)]
        public string DataSource { get; set; }
    }
}