using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.MCPHeaders
{
    public class GetMCPHeadersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? EffectiveDateMin { get; set; }
        public DateTime? EffectiveDateMax { get; set; }
        public DateTime? EndDateMin { get; set; }
        public DateTime? EndDateMax { get; set; }
        public Guid? RouteId { get; set; }
        public Guid? CompanyId { get; set; }

        public GetMCPHeadersInput()
        {

        }
    }
}