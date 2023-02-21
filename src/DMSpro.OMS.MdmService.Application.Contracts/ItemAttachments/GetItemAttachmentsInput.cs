using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.ItemAttachments
{
    public class GetItemAttachmentsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public Guid? FileId { get; set; }
        public Guid? ItemId { get; set; }

        public GetItemAttachmentsInput()
        {

        }
    }
}