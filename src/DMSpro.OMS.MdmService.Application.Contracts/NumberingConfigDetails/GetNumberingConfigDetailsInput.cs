using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.NumberingConfigDetails
{
    public class GetNumberingConfigDetailsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Description { get; set; }
        public string Prefix { get; set; }
        public int? PaddingZeroNumberMin { get; set; }
        public int? PaddingZeroNumberMax { get; set; }
        public string Suffix { get; set; }
        public bool? Active { get; set; }
        public int? CurrentNumberMin { get; set; }
        public int? CurrentNumberMax { get; set; }
        public Guid? NumberingConfigId { get; set; }
        public Guid? CompanyId { get; set; }

        public GetNumberingConfigDetailsInput()
        {

        }
    }
}