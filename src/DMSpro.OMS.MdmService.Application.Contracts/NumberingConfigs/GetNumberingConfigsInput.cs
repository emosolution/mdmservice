using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class GetNumberingConfigsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? PaddingZeroNumberMin { get; set; }
        public int? PaddingZeroNumberMax { get; set; }
        public string Description { get; set; }
        public Guid? SystemDataId { get; set; }

        public GetNumberingConfigsInput()
        {

        }
    }
}