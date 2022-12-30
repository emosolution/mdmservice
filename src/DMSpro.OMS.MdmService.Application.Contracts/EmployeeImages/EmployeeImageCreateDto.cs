using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageCreateDto
    {
        public string Description { get; set; }
        [Required]
        public string url { get; set; }
        public bool Active { get; set; } = true;
        public bool IsAvatar { get; set; } = false;
        public Guid EmployeeProfileId { get; set; }
    }
}