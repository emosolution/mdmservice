using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.EmployeeAttachments
{
    public class GetEmployeeAttachmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? FileId { get; set; }
        public Guid? EmployeeProfileId { get; set; }

        public GetEmployeeAttachmentsInput()
        {

        }
    }
}