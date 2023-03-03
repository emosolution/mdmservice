using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Content;

namespace DMSpro.OMS.MdmService.EmployeeImages
{
    public class EmployeeImageUpdateDto : IHasConcurrencyStamp
    {
        [StringLength(EmployeeImageConsts.DescriptionMaxLength)]
        public string Description { get; set; }
        public bool Active { get; set; }
        //public bool IsAvatar { get; set; }
        [Required]
        public IRemoteStreamContent File { get; set; }
        [Required]
        public Guid EmployeeProfileId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}