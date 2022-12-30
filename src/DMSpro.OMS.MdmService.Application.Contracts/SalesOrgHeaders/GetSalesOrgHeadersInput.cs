using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.SalesOrgHeaders
{
    public class GetSalesOrgHeadersInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public GetSalesOrgHeadersInput()
        {

        }
    }
}