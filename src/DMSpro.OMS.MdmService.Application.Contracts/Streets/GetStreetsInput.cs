using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.Streets
{
    public class GetStreetsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }

        public GetStreetsInput()
        {

        }
    }
}