using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class AvartarCreateDto: ExtensibleObject
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        //public bool IsAvatar { get; set; } = false;
        [Required]
        public IRemoteStreamContent File { get; set; }
        [Required]
        public Guid EmployeeProfileId { get; set; }
    }
}
