using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageCreateDto
    {
        [StringLength(EmployeeImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        [Required]
        public IRemoteStreamContent File { get; set; }
        [Required]
        public Guid EmployeeProfileId { get; set; }
    }
}