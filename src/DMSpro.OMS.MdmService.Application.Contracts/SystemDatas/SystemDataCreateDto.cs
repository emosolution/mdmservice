using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.SystemDatas
{
    public class SystemDataCreateDto
    {
        [Required]
        [StringLength(SystemDataConsts.CodeMaxLength, MinimumLength = SystemDataConsts.CodeMinLength)]
        public string Code { get; set; }
        [Required]
        [StringLength(SystemDataConsts.ValueCodeMaxLength, MinimumLength = SystemDataConsts.ValueCodeMinLength)]
        public string ValueCode { get; set; }
        [Required]
        [StringLength(SystemDataConsts.ValueNameMaxLength, MinimumLength = SystemDataConsts.ValueNameMinLength)]
        public string ValueName { get; set; }
    }
}