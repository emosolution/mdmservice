using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.NumberingConfigs
{
    public class GetNumberingConfigsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public int? StartNumberMin { get; set; }
        public int? StartNumberMax { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? LengthMin { get; set; }
        public int? LengthMax { get; set; }
        public bool? Active { get; set; }
        public Guid? SystemDataId { get; set; }

        public GetNumberingConfigsInput()
        {

        }
    }
}