using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerAttachments
{
    public class GetCustomerAttachmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string url { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? CustomerId { get; set; }

        public GetCustomerAttachmentsInput()
        {

        }
    }
}