using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionCreateDto
    {
        [Required]
        [StringLength(WorkingPositionConsts.CodeMaxLength, MinimumLength = WorkingPositionConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(WorkingPositionConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(WorkingPositionConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; } = true;
    }
}