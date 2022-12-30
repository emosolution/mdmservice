using Volo.Abp.Application.Dtos;
using System;

namespace DMSpro.OMS.MdmService.UOMGroups
{
    public class GetUOMGroupsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public GetUOMGroupsInput()
        {

        }
    }
}