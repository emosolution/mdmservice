using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.WorkingPositions
{
    public class WorkingPositionUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(WorkingPositionConsts.CodeMaxLength, MinimumLength = WorkingPositionConsts.CodeMinLength)]
        public string Code { get; set; }
        [StringLength(WorkingPositionConsts.NameMaxLength)]
        public string Name { get; set; }
        [StringLength(WorkingPositionConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}