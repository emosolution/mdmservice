using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class GetNumberingConfigDetailsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public bool? Active { get; set; }
        public string Description { get; set; }
        public Guid? NumberingConfigId { get; set; }
        public Guid? CompanyId { get; set; }

        public GetNumberingConfigDetailsInput()
        {

        }
    }
}