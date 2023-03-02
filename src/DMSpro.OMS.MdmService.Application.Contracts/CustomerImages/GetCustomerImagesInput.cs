using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.CustomerImages
{
    public class GetCustomerImagesInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? IsAvatar { get; set; }
        public bool? IsPOSM { get; set; }
        public Guid? FileId { get; set; }
        public Guid? CustomerId { get; set; }

        public GetCustomerImagesInput()
        {

        }
    }
}