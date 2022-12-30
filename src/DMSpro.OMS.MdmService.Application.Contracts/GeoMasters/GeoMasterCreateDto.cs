using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.GeoMasters
{
    public class GeoMasterCreateDto
    {
        [Required]
        public string Code { get; set; }
        public string ERPCode { get; set; }
        [Required]
        [StringLength(GeoMasterConsts.NameMaxLength, MinimumLength = GeoMasterConsts.NameMinLength)]
        public string Name { get; set; }
        public int Level { get; set; } = 0;
        public Guid? ParentId { get; set; }
    }
}