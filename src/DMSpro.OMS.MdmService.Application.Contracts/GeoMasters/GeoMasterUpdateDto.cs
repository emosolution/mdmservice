using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMasterUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Code { get; set; }
        public string ERPCode { get; set; }
        [Required]
        [StringLength(GeoMasterConsts.NameMaxLength, MinimumLength = GeoMasterConsts.NameMinLength)]
        public string Name { get; set; }
        public int Level { get; set; }
        public Guid? ParentId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}