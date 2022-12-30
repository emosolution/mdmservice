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
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}